using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HussieniSuperMarket.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public byte[] ProductImages { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Products { get; set; }
    }
}
