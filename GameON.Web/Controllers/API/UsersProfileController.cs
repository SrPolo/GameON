using GameON.Common.Models;
using GameON.Web.Data;
using GameON.Web.Data.Entities;
using GameON.Web.Helpers;
using GameON.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameON.Web.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersProfileController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public UsersProfileController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/UsersProfile/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity userEntity = await _context.Users.Include(u => u.VideoGame).FirstOrDefaultAsync(u => u.Id == id.ToString());

            if (userEntity == null)
            {
                return BadRequest(Resource.UserDoesntExists);
            }

            UserResponse userResponse = _converterHelper.ToUserResponseProfile(userEntity);


            return Ok(userResponse);
        }


        // GET: api/UsersProfile/
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<UserEntity> userEntity = await _context.Users.Include(u => u.VideoGame).ToListAsync();

            if (userEntity == null)
            {
                return NotFound();
            }

            return Ok(_converterHelper.ToUserResponseProfile(userEntity));
        }
    }
}
