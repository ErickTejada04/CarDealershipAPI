using DealershipAPI.DTOs;
using DealershipAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DealershipAPI.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }


        /*
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            if (UserExists(user.Username) || String.IsNullOrEmpty(user.Username))
            {
                return BadRequest();
            }

            

            var newUser = new UserEntity
            {
                UserID = Guid.NewGuid().ToString(),
                Username = user.Username,
                Hash = user.Hash,
                Salt = user.Salt,
                Active = 1,
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(newUser);
        }

        private bool UserExists(string username)
        {
            return _context.User.Any(e => e.Username == username);
        }

        */
    }
}
