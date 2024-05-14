
using HussieniSuperMarket.Models;

namespace HussieniSuperMarket.Services.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductAsync();
        Task<List<Product>> GetAllOutOfStockAsync();

        Task<List<Product>> GetProductWithMainCategoryAsync(string maincategory);
        Task<List<Product>> GetProductWithSubCategoryAsync(string subcategory);
        Task<List<Product>> GetProductWithCategoryAsync(string category);
        Task<List<Product>> GetProductBrand(string brandname);

        Task<Product> GetProductAsync(int productId);

        Task<Product> CreateProductAsync(Product productModel);

        Task<Product> UpdateProductAsync(Product productModel);

        Task<Product> DeleteProductAsync(int productId);

        Task<bool> IsCategoryExist(int categoryId);

    }
}
