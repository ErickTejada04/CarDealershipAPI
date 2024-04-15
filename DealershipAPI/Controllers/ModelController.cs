using DealershipAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ModelsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Model
                .Select(Model => new
                {
                    ModelID = Model.ModelID,
                    ModelName = Model.Model,
                    BrandName = Model.Brand.Brand,
                    Body = Model.Body.Body,
                })
                .ToList()); 
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ModelEntity model)
        {
            var newModel = new ModelEntity
            {
                ModelID = model.ModelID,
                Model = model.Model,
                BodyID = model.BodyID,
                Body = model.Body,
                BrandID = model.BrandID,
                Brand   = model.Brand,
            };
            _context.Model.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ModelEntity model)
        {
            if (id != model.ModelID)
            {
                return NotFound();
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _context.Model.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Model.Remove(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ModelExists(string id)
        {
            return _context.Model.Any(e => e.ModelID == id);
        }
    }
}