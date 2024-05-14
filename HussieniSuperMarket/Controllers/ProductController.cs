using AutoMapper;
using Azure;
using HussieniSuperMarket.Data;
using HussieniSuperMarket.Dtos;
using HussieniSuperMarket.Dtos.ProductDtos;
using HussieniSuperMarket.Models;
using HussieniSuperMarket.Services;
using HussieniSuperMarket.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace HussieniSuperMarket.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IProductRepository _productRepo;
        private IMapper _mapper;

        public ProductController(AppDbContext db,IMapper mapper,IProductRepository productRepository)
        {
            _db = db;
            _productRepo = productRepository;
            _mapper = mapper;
           

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            var products = await _productRepo.GetAllProductAsync();
            if(products==null || products.Count == 0)
            {
                return NotFound("No Products Found");
            }
            var response = _mapper.Map<List<ProductDto>>(products);


            return Ok(response);
        }

        [HttpGet("OutofStockProducts")]
        public async Task<IActionResult> OutofStockProducts()
        {

            var products = await _productRepo.GetAllOutOfStockAsync();

            if (products == null)
            {
                return NotFound("No Products Found");
            }
            var response = _mapper.Map<List<ProductDto>>(products);


            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product=await _productRepo.GetProductAsync(id);
            if (product == null)
            {
                return NotFound($"No ProductId:{id} Found");
            }
            var response = _mapper.Map<ProductDto>(product);
          
            return Ok(response);
        }

        [HttpGet("GetOffers")]
        public async Task<IActionResult> GetOffers()
        {
            var products = await _db.Products.Where(c => c.Discount > 0).ToListAsync();
            if (products.Count == 0)
            {
                return NotFound("No products with discounts found.");
            }
            var response = _mapper.Map<List<ProductDto>>(products);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetProductWithMainCategoryAsync")]
        public async Task<IActionResult> GetProductsWithMainCategory(string maincategory)
        {

            var products = await _productRepo.GetProductWithMainCategoryAsync(maincategory);
            if (products == null )
            {
                return NotFound("No Products Found");
            }
            var response = _mapper.Map<List<ProductDto>>(products);


            return Ok(response);
        }

        [HttpGet]
        [Route("GetProductWithSubCategoryAsync")]
        public async Task<IActionResult> GetProductsWithSubCategory(string subcategory)
        {

            var products = await _productRepo.GetProductWithSubCategoryAsync(subcategory);
            if (products == null)
            {
                return NotFound("No Products Found");
            }
            var response = _mapper.Map<List<ProductDto>>(products);


            return Ok(response);
        }

        [HttpGet]
        [Route("GetProductWithCategoryAsync")]
        public async Task<IActionResult> GetProductsWithCategory(string category)
        {

            var products = await _productRepo.GetProductWithCategoryAsync(category);
            if (products == null)
            {
                return NotFound("No Products Found");
            }
            var response = _mapper.Map<List<ProductDto>>(products);


            return Ok(response);
        }

        [HttpGet]
        [Route("GetProductsByBrandNameAsync")]
        public async Task<IActionResult> GetProductsByBrandName(string brandname)
        {

            var products = await _productRepo.GetProductBrand(brandname);
            if (products == null)
            {
                return NotFound("No Products Found");
            }
            var response = _mapper.Map<List<ProductDto>>(products);


            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductDto createproductDto)
        {
            Product productModel=_mapper.Map<Product>(createproductDto);
            
            await _productRepo.CreateProductAsync(productModel);

            var productWithCategories = await _productRepo.GetProductAsync(productModel.Id);
            var response = _mapper.Map<ProductDto>(productWithCategories);


            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productRepo.GetProductAsync(updateProductDto.Id);

            if (existingProduct == null)
            {
                return NotFound($"No ProductId:{updateProductDto.Id} Found");
            }

            var isCategoryExist = await _productRepo.IsCategoryExist(updateProductDto.CategoryId);

            if (!isCategoryExist)
            {
                return NotFound($"Category with ID {updateProductDto.CategoryId} not found");
            }




            // Map the updated product DTO to the existing product entity
            _mapper.Map(updateProductDto, existingProduct);

            
            var updatedProduct = await _productRepo.UpdateProductAsync(existingProduct);

            
            var response = _mapper.Map<ProductDto>(updatedProduct);

            return Ok(response);
        }



        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            var product=await _productRepo.DeleteProductAsync(id);

            if (product == null)
            {
                return NotFound($"The product Id:{id} Not Found");
            }
           

            return NoContent();

        }

    }
}
