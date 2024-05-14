using HussieniSuperMarket.Data;
using HussieniSuperMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HussieniSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductImageController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet("GetProductImages")]
        public async Task<IActionResult> GetProductImages(int productId)
        {
            try
            {
                var product = await _db.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                List<string> imageUrls = new List<string>();
                foreach (var image in product.Images)
                {
                    imageUrls.Add(Convert.ToBase64String(image.ProductImages));
                }

                return Ok(imageUrls);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("upload/{productId}")]
        public async Task<IActionResult> Upload(int productId, IFormFileCollection files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest("No files uploaded.");
                }

                var product = await _db.Products.FindAsync(productId);
                if (product == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                foreach (var file in files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var image = new ProductImage
                        {
                            ProductId = productId,
                            ProductImages = memoryStream.ToArray()
                        };
                        _db.ProductImages.Add(image);
                    }
                }

                await _db.SaveChangesAsync();

                return Ok("Files uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }

        }
        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProductImage(int productId, IFormFile file)
        {
            try
            {
                var product = await _db.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                var image = product.Images.FirstOrDefault(); // Assuming there's only one image per product

                if (image == null)
                {
                    return NotFound($"No image found for Product with ID {productId}.");
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    image.ProductImages = memoryStream.ToArray();
                }

                await _db.SaveChangesAsync();

                return Ok("Image updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }






    }
}
