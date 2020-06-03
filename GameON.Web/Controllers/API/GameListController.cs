using GameON.Common.Models;
using GameON.Web.Data;
using GameON.Web.Data.Entities;
using GameON.Web.Helpers;
using GameON.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class GameListController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public GameListController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> AddGametoList([FromBody] GameListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            VideoGameEntity videoGame = await _context.VideoGames.FindAsync(request.VideoGameId);
            if (videoGame == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.VideoGameDoesntExists
                });
            }

            UserEntity user = await _userHelper.GetUserAsync(request.Userid);
            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.UserDoesntExists
                });
            }


            GameListEntity gameList = await _context.GameLists.FirstOrDefaultAsync(g => g.User.Id == request.Userid.ToString() && g.VideoGame.Id == request.VideoGameId);



            if (gameList == null)
            {
                gameList = new GameListEntity
                {
                    status = request.status,
                    VideoGame = videoGame,
                    User = user
                };

                _context.GameLists.Add(gameList);
            }
            else
            {
                gameList.status = request.status;
                gameList.User = user;
                gameList.VideoGame = videoGame;
                _context.GameLists.Update(gameList);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("GetGameListForUser")]
        public async Task<IActionResult> GetGameListForUser([FromBody] GameListForUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            UserEntity userEntity = await _context.Users
                .Include(u => u.GameList)
                .ThenInclude(g => g.VideoGame)
                .ThenInclude(v => v.Developers)
                .ThenInclude(d => d.Developer)
                .Include(u => u.GameList)
                .ThenInclude(g => g.VideoGame)
                .ThenInclude(v => v.Genres)
                .ThenInclude(d => d.Genre)
                .Include(u => u.GameList)
                .ThenInclude(g => g.VideoGame)
                .ThenInclude(v => v.Platforms)
                .ThenInclude(d => d.Platform)
                .FirstOrDefaultAsync(u => u.Id == request.UserId.ToString());
            if (userEntity == null)
            {
                return BadRequest(Resource.UserDoesntExists);
            }

            List<GameListResponse> gamelist = new List<GameListResponse>();

            foreach (GameListEntity gameList in userEntity.GameList)
            {
                gamelist.Add(_converterHelper.ToGameListResponse(gameList));
            }

            return Ok(gamelist.OrderBy(g => g.VideoGame.Name));
        }



    }

}
