using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HussieniSuperMarket.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }=string.Empty;

        public string BrandName {  get; set; }=string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Size { get; set; } = 0;
        public string SizeUnit {  get; set; } = string.Empty;

        [Range(0, 100)]
        public int Discount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")] 
        public decimal price { get; set; }

        public int ProductQuantity { get; set; } = 0;
        
        //Navigation
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public Category?Categories { get; set; }

        public List<ProductImage> ?Images { get; set; }


    }
}
