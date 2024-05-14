using AutoMapper;
using HussieniSuperMarket.Dtos.CategoryDtos;
using HussieniSuperMarket.Dtos.MainCategoryDtos;
using HussieniSuperMarket.Dtos.OrderDtos;
using HussieniSuperMarket.Dtos.OrderProductDtos;
using HussieniSuperMarket.Dtos.ProductDtos;
using HussieniSuperMarket.Dtos.SubCategoryDtos;
using HussieniSuperMarket.Models;


namespace HussieniSuperMarket.Dtos
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() 
        { 
         var mappingConfig=new MapperConfiguration(config =>
         {
             config.CreateMap<ProductDto, Product>();
             config.CreateMap<Product, ProductDto>();
             config.CreateMap<CreateProductDto, Product>();
             config.CreateMap<Product, CreateProductDto>();
             config.CreateMap<UpdateProductDto, Product>();
             config.CreateMap<Product, UpdateProductDto>();
             config.CreateMap<CategoryDto, Category>();
             config.CreateMap<Category, CategoryDto>();
             config.CreateMap<SubCategoryDto, SubCategory>();
             config.CreateMap<SubCategory, SubCategoryDto>();
             config.CreateMap<MainCategoryDto, MainCategory>();
             config.CreateMap<MainCategory, MainCategoryDto>();
             config.CreateMap<OrderDto, Order>();
             config.CreateMap<Order, OrderDto>();
             config.CreateMap<CreateOrderDto, Order>();
             config.CreateMap<Order, CreateOrderDto>();

             config.CreateMap<OrderProductDto, OrderProduct>();
             config.CreateMap<OrderProduct, OrderProductDto>();
             config.CreateMap<CreateOrderProductDto, OrderProduct>();
             config.CreateMap<OrderProduct, CreateOrderProductDto>();
         });
            return mappingConfig;
        }
        
    }
}
