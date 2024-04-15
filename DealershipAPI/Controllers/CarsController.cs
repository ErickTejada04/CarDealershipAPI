using DealershipAPI.DTOs;
using DealershipAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Car
                .Include(model => model.Model)
                .Include(brand => brand.Model.Brand)
                .Include(body => body.Model.Body)
                .ToList());
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarDTO car)
        {
            var model = _context.Model.FirstOrDefault(x => x.Model == car.ModelName);

            var newCar = new CarEntity
            {
                CarID = Guid.NewGuid().ToString(),
                Model = model,
                ModelID = model.ModelID,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                Condition = car.Condition,
                Mileage = car.Mileage,
                Traction = car.Traction,
                Transmission = car.Transmission,
                Description = car.Description,
                CreationDate = DateTime.Now,
                Doors = car.Doors,
            };
            _context.Car.Add(newCar);
            await _context.SaveChangesAsync();
            return Ok(car);
        }
        
        [HttpGet("{id}")]
        public async Task <IActionResult> Get(string id)
        {
            if (!CarExists(id))
            {
                return NotFound();
            }
            //Develop, ahora en controlador
            var car = await _context.Car
                .Include(model => model.Model)
                .Include(brand => brand.Model.Brand)
                .Include(body => body.Model.Body)
                .FirstOrDefaultAsync(x => x.CarID == id);
            return Ok(car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] CarDTO car)
        {
            if (!CarExists(id))
            {
                return NotFound();
            }

            try
            {

                var model = _context.Model.FirstOrDefault(x => x.Model == car.ModelName);

                var carEntity = await _context.Car.FirstOrDefaultAsync(x => x.CarID == id);
                carEntity.Model = model;
                carEntity.ModelID = model.ModelID;
                carEntity.Year = car.Year;
                carEntity.Color = car.Color;
                carEntity.Price = car.Price;
                carEntity.Condition = car.Condition;
                carEntity.Mileage = car.Mileage;
                carEntity.Traction = car.Traction;
                carEntity.Transmission = car.Transmission;
                carEntity.Description = car.Description;
                carEntity.Doors = car.Doors;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }


            return Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!CarExists(id))
            {
                return NotFound();
            }

            var car = await _context.Car.FirstOrDefaultAsync(x => x.CarID == id);
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return Ok();
        }
        

        private bool CarExists(string id)
        {
            return _context.Car.Any(e => e.CarID == id);
        }   
    }

}
