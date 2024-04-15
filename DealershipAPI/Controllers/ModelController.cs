using DealershipAPI.DTOs;
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
        public async Task<IActionResult> Post([FromBody] ModelDTO model)
        {
            var Brand = _context.Brand.FirstOrDefault(x => x.Brand == model.Brand);
            var Body = _context.Body.FirstOrDefault(x => x.Body == model.Body);


            var newModel = new ModelEntity
            {
                ModelID = Guid.NewGuid().ToString(),
                Model = model.Model,
                Body = Body,
                Brand = Brand,
            };
            _context.Model.Add(newModel);
            await _context.SaveChangesAsync();
            return Ok(newModel);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ModelDTO model)
        {
            if (!ModelExists(id))
            {
                return NotFound();
            }

            try
            {
                var Brand = _context.Brand.FirstOrDefault(x => x.Brand == model.Brand);
                var Body = _context.Body.FirstOrDefault(x => x.Body == model.Body);
                var modelEntity = await _context.Model.FirstOrDefaultAsync(x => x.ModelID == id);

                modelEntity.Model = model.Model;
                modelEntity.Brand = Brand;
                modelEntity.Body = Body;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok(model);
        }

        /*[HttpDelete("{id}")]
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
        }*/

        private bool ModelExists(string id)
        {
            return _context.Model.Any(e => e.ModelID == id);
        }
    }
}