using HussieniSuperMarket.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HussieniSuperMarket.Dtos.OrderProductDtos
{
    public class CreateOrderProductDto
    {
        
       

        public int OrderId { get; set; }

        public int ProductId { get; set; }
 

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
