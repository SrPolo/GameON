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
    public class PlatformsController : Controller
    {
        private readonly DataContext _context;

        public PlatformsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Platforms.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatformEntity platformEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platformEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"The platform '{platformEntity.Name}' already exists");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                    }
                }
            }
            return View(platformEntity);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlatformEntity platformEntity = await _context.Platforms.FindAsync(id);
            if (platformEntity == null)
            {
                return NotFound();
            }
            return View(platformEntity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlatformEntity platformEntity)
        {
            if (id != platformEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(platformEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"The platform '{platformEntity.Name}' already exists");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                    }
                }
            }
            return View(platformEntity);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlatformEntity platformEntity = await _context.Platforms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platformEntity == null)
            {
                return NotFound();
            }
            try
            {
                _context.Platforms.Remove(platformEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        private bool PlatformExists(int id)
        {
            return _context.Platforms.Any(e => e.Id == id);
        }
    }
}
