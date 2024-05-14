using HussieniSuperMarket.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HussieniSuperMarket.Dtos.OrderDtos
{
    public class OrderDto
    {
        
        public int Id { get; set; }

        public string UserId { get; set; }
       
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; } = 0;
    }
}
