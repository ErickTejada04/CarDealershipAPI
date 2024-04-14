using DealershipAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DealershipAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CarsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Brand);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var car = _context.Brand.Find(id);
            if (car == null)
            {
                return NotFound();
            }
            //Develop, ahora en controlador
            return Ok(car);
        }
    }

}
