﻿using DealershipAPI.DTOs;
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
        public async Task<IActionResult> Get([FromQuery] CarParameters carParameters)
        {

            var carQuery = await _context.Car
                .Select(car => new CarResponse{
                    CarID = car.CarID,
                    ModelName = car.Model.Model,
                    BrandName = car.Model.Brand.Brand,
                    BodyName = car.Model.Body.Body,
                    Year = car.Year,
                    Color = car.Color,
                    Price = car.Price,
                    Condition = car.Condition,
                    Mileage = car.Mileage,
                    Traction = car.Traction,
                    Transmission = car.Transmission,
                    Description = car.Description,
                    Doors = car.Doors
                }).ToListAsync();

            if (carQuery.Count == 0)
            {
                return NotFound();
            }

            if (carParameters.Brand != null) carQuery = carQuery.Where(x => x.BrandName == carParameters.Brand).ToList();
            if (carParameters.Model != null) carQuery = carQuery.Where(x => x.ModelName == carParameters.Model).ToList();
            if (carParameters.Body != null) carQuery = carQuery.Where(x => x.BodyName == carParameters.Body).ToList();
            if (carParameters.Condition != null) carQuery = carQuery.Where(x => x.Condition == carParameters.Condition).ToList();
            if (carParameters.Year != null) carQuery = carQuery.Where(x => x.Year <= carParameters.Year).ToList();
            if (carParameters.Price != null) carQuery = carQuery.Where(x => x.Price <= carParameters.Price).ToList();
            if (carParameters.Mileage != null) carQuery = carQuery.Where(x => x.Mileage <= carParameters.Mileage).ToList();
            if (carParameters.Transmission != null) carQuery = carQuery.Where(x => x.Traction == carParameters.Transmission).ToList();
            if (carParameters.SortBy == "Price") carQuery = carQuery.OrderBy(x => x.Price).ToList();
            if (carParameters.Keyword != null) carQuery = carQuery.Where(x => x.BrandName.Contains(carParameters.Keyword) 
            || x.ModelName.Contains(carParameters.Keyword)
            || x.BodyName.Contains(carParameters.Keyword)
            || x.Color.Contains(carParameters.Keyword)
            ).ToList();


            var cars = carQuery
                .Skip((carParameters.PageNumber - 1) * carParameters.PageSize)
                .Take(carParameters.PageSize)
                .ToList();


            return Ok(cars);
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
            var car = await _context.Car
                .Select(car => new CarResponse
                {
                    CarID = car.CarID,
                    ModelName = car.Model.Model,
                    BrandName = car.Model.Brand.Brand,
                    BodyName = car.Model.Body.Body,
                    Year = car.Year,
                    Color = car.Color,
                    Price = car.Price,
                    Condition = car.Condition,
                    Mileage = car.Mileage,
                    Traction = car.Traction,
                    Transmission = car.Transmission,
                    Description = car.Description,
                    Doors = car.Doors
                }).FirstOrDefaultAsync(x => x.CarID == id);
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

        /*
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
        */

        private bool CarExists(string id)
        {
            return _context.Car.Any(e => e.CarID == id);
        }   
    }

}
