using HussieniSuperMarket.Models;

namespace HussieniSuperMarket.Dtos.OrderProductDtos
{
    public class ProductSalesDto
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal TotalSalesAmount { get; set; }
    }
}
