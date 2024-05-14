using AutoMapper;
using HussieniSuperMarket.Data;
using HussieniSuperMarket.Dtos.ProductDtos;
using HussieniSuperMarket.Models;
using HussieniSuperMarket.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HussieniSuperMarket.Services
{
    public class ProductRepository : IProductRepository
    {  private readonly AppDbContext _db;
      
        public ProductRepository(AppDbContext db)
        {
            _db = db;
           
        }

        public async Task<Product> CreateProductAsync(Product productModel)
        {
            await _db.Products.AddAsync(productModel);
            await _db.SaveChangesAsync();
            return productModel;
        }

        public async  Task<Product> DeleteProductAsync(int productId)
        {
            var productModel = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (productModel == null)
            {
                return null;
            }
            _db.Products.Remove(productModel);
            await _db.SaveChangesAsync();
            return productModel ;
        }

        public Task<List<Product>> GetAllOutOfStockAsync()
        {
          var product=_db.Products.Where(c=>c.ProductQuantity<=50).ToListAsync();
          return product;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _db.Products.Include(c => c.Categories).ThenInclude(c => c.SubCategories).ThenInclude(c=>c.MainCategories).ToListAsync();
        }

        

        public async Task<Product> GetProductAsync(int productId)
        {    

            var product= await _db.Products.Include(c => c.Categories).ThenInclude(c => c.SubCategories).ThenInclude(c => c.MainCategories).FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<List<Product>> GetProductBrand(string brandname)
        {
           var product= await _db.Products
                .Include(c=>c.Categories)
                .ThenInclude(c => c.SubCategories)
                .ThenInclude(c => c.MainCategories)
                .Where(c=>c.BrandName==brandname).ToListAsync();
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<List<Product>> GetProductWithCategoryAsync(string category)
        {
            var product = await _db.Products.Include(c => c.Categories)
                 .Where(c => c.Categories.Name == category).ToListAsync();
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<List<Product>> GetProductWithMainCategoryAsync(string maincategory)
        {
            var product = await _db.Products.Include(c => c.Categories)
                .ThenInclude(c => c.SubCategories).
                ThenInclude(c => c.MainCategories)
                .Where(c=>c.Categories.SubCategories.MainCategories.MainCategoryName== maincategory).ToListAsync();
            if (product == null)
            {
                return null;
            }
            return product;

        }

        public async Task<List<Product>> GetProductWithSubCategoryAsync(string subcategory)
        {
            var product = await _db.Products.Include(c => c.Categories)
                 .ThenInclude(c => c.SubCategories)
                 .Where(c => c.Categories.SubCategories.SubCategoryName == subcategory).ToListAsync();
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<bool> IsCategoryExist(int categoryId)
        {
            return await _db.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<Product> UpdateProductAsync(Product productModel)
        {
          

            _db.Update(productModel); 
            await _db.SaveChangesAsync(); 
            return productModel; 
        }
    }
}
