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
    public class DeveloperController : Controller
    {
        private readonly DataContext _context;

        public DeveloperController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Developers.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeveloperEntity developerEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(developerEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"The developer '{developerEntity.Name}' already exists");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                    }
                }
            }
            return View(developerEntity);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeveloperEntity developerEntity = await _context.Developers.FindAsync(id);
            if (developerEntity == null)
            {
                return NotFound();
            }
            return View(developerEntity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeveloperEntity developerEntity)
        {
            if (id != developerEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(developerEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"The developer '{developerEntity.Name}' already exists");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                    }
                }
            }
            return View(developerEntity);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeveloperEntity developerEntity = await _context.Developers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (developerEntity == null)
            {
                return NotFound();
            }
            try
            {
                _context.Developers.Remove(developerEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        private bool DeveloperExists(int id)
        {
            return _context.Developers.Any(e => e.Id == id);
        }
    }
}
