using DealershipAPI.DTOs;
using DealershipAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealershipAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class BodiesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BodiesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Body
                .Select(Body => new
                {
                    BodyID = Body.BodyID,
                    Body = Body.Body,
                })
                .ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Post(string bodyName)
        {
            if (BodyExists(bodyName) || bodyName.Length == 0 || bodyName == null)
            {
                return BadRequest();
            }

            var Body= _context.Body.FirstOrDefault(x => x.Body == bodyName);

            var newBody = new BodyEntity
            {
                BodyID = Guid.NewGuid().ToString(),
                Body = bodyName,
            };

            _context.Body.Add(newBody);
            await _context.SaveChangesAsync();
            return Ok(newBody);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, string newBodyName)
        {

            if (!BodyExistsByID(id) || newBodyName.Length == 0 || newBodyName == null)
            {
                return NotFound();
            }

            var Body = _context.Body.FirstOrDefault(x => x.BodyID == id);

            Body.Body = newBodyName;
            await _context.SaveChangesAsync();

            return Ok(Body);
        }

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var brand = await _context.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brand.Remove(brand);
            await _context.SaveChangesAsync();

            return Ok();
        }*/

        private bool BodyExists(string bodyName)
        {
            return _context.Body.Any(e => e.Body == bodyName);
        }

        private bool BodyExistsByID(string id)
        {
            return _context.Body.Any(e => e.BodyID == id);
        }
    }
}