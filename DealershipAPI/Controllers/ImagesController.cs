using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DealershipAPI.DTOs;
using DealershipAPI.Entities;
using DealershipAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DealershipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        BlobServiceClient _blobClient;
        BlobContainerClient _containerClient;

        public ImagesController(ApplicationContext context)
        {
            _context = context;

            _blobClient = new BlobServiceClient(Secrets.AzureBlobConnectionString);
            _containerClient = _blobClient.GetBlobContainerClient("carimages");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Image
                .Select(x => new ImageDTO
                {
                    ImageID = x.ImageID,
                    CarID = x.CarID,
                    ImageURL = x.ImageURL,
                    Main = Convert.ToInt32(x.Main)
                }).ToListAsync());
        }

        [HttpPost("{id}/images")]
        public async Task<IActionResult> Post(string id, string? coverImageName, [FromForm] List<IFormFile> files)
        {

            var car = await _context.Car.FirstOrDefaultAsync(e => e.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            List<ImageEntity> images = new List<ImageEntity>();

            foreach (var file in files)
            {
                try { 
                    if (file.Length == 0)
                    {
                        return BadRequest("Empty file");
                    }

                    string fileName = file.FileName;
                    string url = "";
                    BlobClient blob = _containerClient.GetBlobClient(file.FileName);

                    var blobHttpHeader = new BlobHttpHeaders
                    {
                        ContentType = "image/png"
                    };

                    using (Stream stream = file.OpenReadStream())
                    {
                        blob.Upload(stream, blobHttpHeader);
                    }

                    url = blob.Uri.AbsoluteUri;


                    var newImage = new ImageEntity
                    {
                        ImageID = Guid.NewGuid().ToString(),
                        CarID = id,
                        ImageURL = url,
                        Main = coverImageName == fileName ? 1 : 0
                    };


                    if(!URLExists(url))
                    {
                        images.Add(newImage);
                        _context.Image.Add(newImage);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            };

            return Ok(images);
        }


        private bool URLExists(string url)
        {
            return _context.Image.Any(e => e.ImageURL == url);
        }
    }
}
