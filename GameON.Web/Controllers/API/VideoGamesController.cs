using GameON.Web.Data;
using GameON.Web.Data.Entities;
using GameON.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public VideoGamesController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/VideoGames
        [HttpGet]
        public async Task<IActionResult> GetVideoGames()
        {
            List<VideoGameEntity> videogames = await _context.VideoGames
                .Include(g => g.Genres).ThenInclude(g => g.Genre)
                .Include(g => g.Platforms).ThenInclude(g => g.Platform)
                .Include(g => g.Developers).ThenInclude(g => g.Developer)
                .ToListAsync();

            return Ok(_converterHelper.ToVideoGameResponse(videogames));
        }

        // GET: api/VideoGames/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoGameEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VideoGameEntity videoGameEntity = await _context.VideoGames
               .Include(g => g.Genres).ThenInclude(g => g.Genre)
               .Include(g => g.Platforms).ThenInclude(g => g.Platform)
               .Include(g => g.Developers).ThenInclude(g => g.Developer)
               .Where(g => g.Id == id).FirstOrDefaultAsync();


            if (videoGameEntity == null)
            {
                return NotFound();
            }

            return Ok(_converterHelper.ToVideoGameResponse(videoGameEntity));
        }

    }
}