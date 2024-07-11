using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuelFocusAPI.Data;
using FuelFocusAPI.Model;

namespace FuelFocusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UsersController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            IEnumerable<User> users = await _context.Tbl_User_Profile.ToListAsync();
            return Ok(users);
        }

        // GET: api/Users/Details/5
        [HttpGet("{mobileNo}")]
        public async Task<ActionResult<User>> Details(long mobileNo)
        {
            if(mobileNo == null)
            {
                return NotFound();
            }
            var user = await _context.Tbl_User_Profile.FirstOrDefaultAsync(m => m.Mobile_No == mobileNo);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (user != null)
            {
                _context.Tbl_User_Profile.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { mobileNo = user.Mobile_No }, user);
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public async Task<IActionResult> EditPassword([FromBody] User request)
        {
            try
            {
                var user = await _context.Tbl_User_Profile.FirstOrDefaultAsync(m => m.Mobile_No == request.Mobile_No);
                if (user == null)
                {
                    return NotFound();
                }
                user.Updated_On = DateTime.Now;
                user.Password = request.Password;
                user.Name = request.Name;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(request.Mobile_No))
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

        // POST: Users/Delete/5
        [HttpDelete("{mobile_no}")]
        public async Task<IActionResult> DeleteConfirmed(long mobile_no)
        {
            var user = await _context.Tbl_User_Profile.FirstOrDefaultAsync(m => m.Mobile_No == mobile_no);
            if (user == null)
            {
                return NotFound();
            }
            _context.Tbl_User_Profile.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(long mobileNo)
        {
            return _context.Tbl_User_Profile.Any(e => e.Mobile_No == mobileNo);
        }
    }
}
