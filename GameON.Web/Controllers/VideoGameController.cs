using GameON.Web.Data;
using GameON.Web.Data.Entities;
using GameON.Web.Helpers;
using GameON.Web.Models;
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
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public VideoGameController(
            DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoGames.OrderBy(t => t.Name).ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VideoGameEntity videoGameEntity = await _context.VideoGames
                .Include(t => t.Developers).ThenInclude(t=>t.Developer)
                .Include(t =>t.Genres).ThenInclude(t => t.Genre)
                .Include(t=> t.Platforms).ThenInclude(t => t.Platform)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (videoGameEntity == null)
            {
                return NotFound();
            }

            return View(videoGameEntity);
        }

        public IActionResult Create()
        {
           
            VideoGameViewModel model = new VideoGameViewModel
            {
                ReleaseDate = DateTime.Now,
                GenreList = _combosHelper.GetComboGenres(),
                PlatformList = _combosHelper.GetComboPlatforms(),
                DeveloperList =  _combosHelper.GetComboDevelopers() 
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VideoGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.CoverFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.CoverFile, "VideoGames");
                }

                IList<VideoGameGenreEntity> vgGenre = new List<VideoGameGenreEntity>();
                IList<VideoGameDeveloperEntity> vgDeveloper = new List<VideoGameDeveloperEntity>();
                IList<VideoGamePlatformEntity> vgPlatform = new List<VideoGamePlatformEntity>();
                VideoGameEntity videoGame =  _converterHelper.ToVideoGameEntity(model, path, true);

                _context.Add(videoGame);

                foreach (int id in model.GenresId)
                {
                    VideoGameGenreEntity g = new VideoGameGenreEntity()
                    {
                        Genre = await _context.Genres.FindAsync(id),
                        VideoGame = videoGame
                    };
                    vgGenre.Add(g);
                }


                foreach (int id in model.DevelopersId)
                {
                    VideoGameDeveloperEntity g = new VideoGameDeveloperEntity()
                    {
                        Developer = await _context.Developers.FindAsync(id),
                        VideoGame = videoGame
                    };
                    vgDeveloper.Add(g);
                }

                foreach (int id in model.PlatformsId)
                {
                    VideoGamePlatformEntity g = new VideoGamePlatformEntity()
                    {
                        Platform = await _context.Platforms.FindAsync(id),
                        VideoGame = videoGame
                    };
                    vgPlatform.Add(g);
                }

                _context.VGGenres.AddRange(vgGenre);
                _context.VGDevelopers.AddRange(vgDeveloper);
                _context.VGPlatforms.AddRange(vgPlatform);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            model.ReleaseDate = DateTime.Now;
            model.GenreList = _combosHelper.GetComboGenres();
            model.PlatformList = _combosHelper.GetComboPlatforms();
            model.DeveloperList = _combosHelper.GetComboDevelopers();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VideoGameEntity videoGameEntity = await _context.VideoGames
                .Include(t => t.Developers).ThenInclude(t=>t.Developer)
                .Include(t => t.Genres).ThenInclude(t => t.Genre)
                .Include(t => t.Platforms).ThenInclude(t => t.Platform)
                .FirstOrDefaultAsync(m => m.Id == id);

            
            if (videoGameEntity == null)
            {
                return NotFound();
            }

            VideoGameViewModel model = _converterHelper.ToVideoGameViewModel(videoGameEntity);

            foreach (var item in model.Platforms)
            {
                foreach (var item2 in model.PlatformList)
                {
                    if (int.Parse(item2.Value) == item.Platform.Id)
                    {
                        item2.Selected = true;
                        break;
                    }
                }
            }

            foreach (var item in model.Genres)
            {
                foreach (var item2 in model.GenreList)
                {
                    if (int.Parse(item2.Value) == item.Genre.Id)
                    {
                        item2.Selected = true;
                        break;
                    }
                }
            }

            foreach (var item in model.Developers)
            {
                foreach (var item2 in model.DeveloperList)
                {
                    if (int.Parse(item2.Value) == item.Developer.Id)
                    {
                        item2.Selected = true;
                        break;
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VideoGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.PicturePath;

                if (model.CoverFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.CoverFile, "VideoGames");
                }

                VideoGameEntity videoGame =  _converterHelper.ToVideoGameEntity(model, path, false);
                _context.Update(videoGame);
                _context.VGPlatforms.RemoveRange(_context.VGPlatforms.Where(x => x.VideoGame == videoGame));
                _context.VGGenres.RemoveRange(_context.VGGenres.Where(x => x.VideoGame == videoGame));
                _context.VGDevelopers.RemoveRange(_context.VGDevelopers.Where(x => x.VideoGame == videoGame));


                foreach (int id in model.GenresId)
                {
                    VideoGameGenreEntity vgGenre = new VideoGameGenreEntity()
                    {
                        Genre = await _context.Genres.FindAsync(id),
                        VideoGame = videoGame
                    };
                    _context.VGGenres.Update(vgGenre);
                }

                foreach (int id in model.DevelopersId)
                {
                    VideoGameDeveloperEntity vgDeveloper = new VideoGameDeveloperEntity() 
                    { 
                        Developer = await _context.Developers.FindAsync(id),
                        VideoGame = videoGame                
                    };
                    _context.VGDevelopers.Update(vgDeveloper);
                }

                foreach (int id in model.PlatformsId)
                {
                    VideoGamePlatformEntity vgPlatform = new VideoGamePlatformEntity()
                    {
                        Platform = await _context.Platforms.FindAsync(id),
                        VideoGame = videoGame
                    };
                    _context.VGPlatforms.Update(vgPlatform);
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, $"The video game: {videoGame.Name} already exists");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VideoGameEntity videoGameEntity = await _context.VideoGames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoGameEntity == null)
            {
                return NotFound();
            }
            _context.VGPlatforms.RemoveRange(_context.VGPlatforms.Where(x => x.VideoGame == videoGameEntity));
            _context.VGGenres.RemoveRange(_context.VGGenres.Where(x => x.VideoGame == videoGameEntity));
            _context.VGDevelopers.RemoveRange(_context.VGDevelopers.Where(x => x.VideoGame == videoGameEntity));
            _context.VideoGames.Remove(videoGameEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
