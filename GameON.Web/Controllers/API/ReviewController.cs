using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameON.Web.Data;
using GameON.Web.Data.Entities;

namespace GameON.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DataContext _context;

        public ReviewController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Review
        [HttpGet("{id}")]
        [Route("GetReviewsForGame")]
        public IEnumerable<ReviewEntity> GetReviewsForGame([FromRoute] int id)
        {
            return _context.Reviews.Where(r=>r.VideoGame.Id==id);
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewEntity = await _context.Reviews
                .Include(r => r.User)
                .Include(r=>r.VideoGame)
                .Where(r=>r.Id==id)
                .FirstOrDefaultAsync();

            if (reviewEntity == null)
            {
                return NotFound();
            }

            return Ok(reviewEntity);
        }

        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviewEntity([FromRoute] int id, [FromBody] ReviewEntity reviewEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reviewEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(reviewEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Review
        [HttpPost]
        public async Task<IActionResult> PostReviewEntity([FromBody] ReviewEntity reviewEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Reviews.Add(reviewEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviewEntity", new { id = reviewEntity.Id }, reviewEntity);
        }

        
        private bool ReviewEntityExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}