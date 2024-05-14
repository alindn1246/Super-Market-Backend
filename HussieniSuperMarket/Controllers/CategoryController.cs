using HussieniSuperMarket.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HussieniSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]

        public async Task<IActionResult> GetCategoryBySub(string sub)
        {
            var category= await _db.Categories.Include(c=>c.SubCategories).Where(c=>c.SubCategories.SubCategoryName==sub).ToListAsync();
            if (category == null )
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}
