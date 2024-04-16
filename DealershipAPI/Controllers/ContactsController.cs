using DealershipAPI.DTOs;
using DealershipAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ContactsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Contact.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var contact = await _context.Contact.FirstOrDefaultAsync(e => e.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDTO contact)
        {
            var newContact = new ContactEntity
            {
                ContactID = Guid.NewGuid().ToString(),
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Message = contact.Message,
                Status = "Pendiente"
            };

            _context.Contact.Add(newContact);
            await _context.SaveChangesAsync();

            return Ok(newContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var contact = await _context.Contact.FirstOrDefaultAsync(e => e.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.Status = "Archivado";
            await _context.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
