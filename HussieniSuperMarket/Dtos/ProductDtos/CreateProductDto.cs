using HussieniSuperMarket.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HussieniSuperMarket.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string ProductName { get; set; } = string.Empty;

        public string BrandName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string SizeUnit { get; set; } = string.Empty;
        public int Size { get; set; } = 0;

        public int Discount { get; set; } = 0;

        public decimal price { get; set; }

        public int ProductQuantity { get; set; } = 0;

        public int CategoryId { get; set; }
    }
}
