using DealershipAPI.DTOs;
using DealershipAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DealershipAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _context.User.Where(x => x.Active == 1).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            if (UserExists(user.Username) || String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            var salt = GenerateSalt();
            var hash = HashPassword(user.Password, salt);
           
            var newUser = new UserEntity
            {
                UserID = Guid.NewGuid().ToString(),
                Username = user.Username,
                Hash = hash,
                Salt = salt,
                Active = 1,
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var userEntity = await _context.User.FirstOrDefaultAsync(e => e.Username == user.Username);
            if (userEntity == null || userEntity.Active == 0)
            {
                return NotFound();
            }

            var hash = HashPassword(user.Password, userEntity.Salt);
            if (hash != userEntity.Hash)
            {
                return Unauthorized();
            }

            return Ok(userEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userEntity = await _context.User.FirstOrDefaultAsync(e => e.UserID == id);
            if (userEntity == null)
            {
                return NotFound();
            }

            userEntity.Active = 0;
            await _context.SaveChangesAsync();

            return Ok(userEntity);
        }

        private string HashPassword(string password, string salt)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private string GenerateSalt()
        {
            var salt = new byte[128];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        private bool UserExists(string username)
        {
            return _context.User.Any(e => e.Username == username);
        }

       
    }
}
