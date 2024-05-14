using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HussieniSuperMarket.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(SubCategory))]
        public int? SubCategoryId { get; set; }
        public SubCategory? SubCategories { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
