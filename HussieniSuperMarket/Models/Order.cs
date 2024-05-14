using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HussieniSuperMarket.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }=DateTime.Now;

        public decimal TotalAmount { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
