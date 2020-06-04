using GameON.Web.Data;
using GameON.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {

        private readonly DataContext _context;

        public GenresController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreEntity genreEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genreEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"The genre '{genreEntity.Genre}' already exists");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                    }
                }
            }
            return View(genreEntity);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenreEntity genreEntity = await _context.Genres.FindAsync(id);
            if (genreEntity == null)
            {
                return NotFound();
            }
            return View(genreEntity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenreEntity genreEntity)
        {
            if (id != genreEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(genreEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"The genre '{genreEntity.Genre}' already exists");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                    }
                }
            }
            return View(genreEntity);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenreEntity genreEntity = await _context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreEntity == null)
            {
                return NotFound();
            }
            try
            {
                _context.Genres.Remove(genreEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
