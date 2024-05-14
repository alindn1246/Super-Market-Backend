using HussieniSuperMarket.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HussieniSuperMarket.Dtos.OrderProductDtos
{
    public class OrderProductDto
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal TotalAmount => Quantity * Price;
    }
}
