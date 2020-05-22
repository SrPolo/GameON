using GameON.Web.Data;
using GameON.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Controllers
{
    public class VideoGameController : Controller
    {
        private readonly DataContext _context;

        public VideoGameController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoGames.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VideoGameEntity videoGameEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoGameEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"The expense '{videoGameEntity.Name}' already exists");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                    }
                }
            }
            return View(videoGameEntity);
        }
    }
}
