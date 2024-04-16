using DealershipAPI.DTOs;
using DealershipAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealershipAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class BrandsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BrandsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? status)
        {
            var brands = _context.Brand.Select(Brand => new
            {
                BrandID = Brand.BrandID,
                BrandName = Brand.Brand,
                Status = Brand.Status
            }).ToList();

            if (status != null)
            {
                brands = brands.Where(x => x.Status == status).ToList();
            }

            return Ok(brands);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string brandName)
        {
            if (BrandExists(brandName) || brandName.Length == 0 || brandName == null)
            {
                return BadRequest();
            }

            var Brand = _context.Brand.FirstOrDefault(x => x.Brand == brandName);

            var newBrand = new BrandEntity
            {
                BrandID = Guid.NewGuid().ToString(),
                Brand = brandName,
                Status = 1
            };

            _context.Brand.Add(newBrand);
            await _context.SaveChangesAsync();
            return Ok(newBrand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, string newBrandName)
        {
            
            if (!BrandExistsByID(id) || newBrandName.Length == 0 || newBrandName == null)
            {
                return NotFound();
            }

                var Brand = _context.Brand.FirstOrDefault(x => x.BrandID == id);

                Brand.Brand = newBrandName;
                await _context.SaveChangesAsync();

            return Ok(Brand);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SetActive(string id, int status)
        {
            var brand = await _context.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            brand.Status = status;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool BrandExists(string brandName)
        {
            return _context.Brand.Any(e => e.Brand == brandName);
        }

        private bool BrandExistsByID(string id)
        {
            return _context.Brand.Any(e => e.BrandID == id);
        }
    }
}