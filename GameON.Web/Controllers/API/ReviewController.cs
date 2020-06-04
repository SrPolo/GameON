using GameON.Common.Models;
using GameON.Web.Helpers;
using GameON.Web.Data;
using GameON.Web.Data.Entities;
using GameON.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GameON.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public ReviewController(DataContext context, IConverterHelper converterHelper,IUserHelper userHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        // GET: api/Review/GetReviewsForGame/5
        [HttpGet("GetReviewsForGame/{id}")]
        public async Task<IActionResult> GetReviewsForGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VideoGameEntity videoGameEntity = await _context.VideoGames
                .Include(v => v.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (videoGameEntity == null)
            {
                return BadRequest(Resource.VideoGameDoesntExists);
            }

            List<ReviewResponse> reviewResponses = _converterHelper.ToReviewResponse(videoGameEntity.Reviews.ToList());


            return Ok(reviewResponses);
        }

        // GET: api/Review/GetUserReviews/5
        [HttpGet("GetUserReviews/{id}")]
        public async Task<IActionResult> GetUserReviews([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<ReviewEntity> reviewEntities = await _context.Reviews
                .Include(r => r.VideoGame)
                .Include(r=>r.User)
                .Where(r=>r.User.Id==id.ToString())
                .ToListAsync();

            if (reviewEntities == null)
            {
                return BadRequest("El usuario no tiene reviews");
            }

            List<ReviewResponse> reviewResponses = _converterHelper.ToReviewResponse(reviewEntities);


            return Ok(reviewResponses);
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReviewEntity reviewEntity = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.VideoGame)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (reviewEntity == null)
            {
                return NotFound();
            }

            return Ok(_converterHelper.ToReviewResponse(reviewEntity));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] ReviewRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            VideoGameEntity videoGameEntity = await _context.VideoGames.FindAsync(request.VideoGameId);
            if (videoGameEntity == null)
            {
                return BadRequest(Resource.VideoGameDoesntExists);
            }

            UserEntity userEntity = await _userHelper.GetUserAsync(request.UserId);
            if (userEntity == null)
            {
                return BadRequest(Resource.UserDoesntExists);
            }


            ReviewEntity reviewEntity = await _context.Reviews
                .FirstOrDefaultAsync(p => p.User.Id == request.UserId.ToString() && p.VideoGame.Id == request.VideoGameId);

            if (reviewEntity == null)
            {
                reviewEntity = new ReviewEntity
                {
                    Score = request.Score,
                    Review = request.Review,
                    VideoGame = videoGameEntity,
                    User = userEntity
                };

                _context.Reviews.Add(reviewEntity);
            }
            else
            {
                reviewEntity.Score = request.Score;
                reviewEntity.Review = request.Review;
                _context.Reviews.Update(reviewEntity);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // GET: api/Review/
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<ReviewEntity> reviewEntity = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.VideoGame)
                .ToListAsync();

            if (reviewEntity == null)
            {
                return NotFound();
            }

            return Ok(_converterHelper.ToReviewResponse(reviewEntity));
        }

    }
}