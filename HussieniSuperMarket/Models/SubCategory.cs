using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HussieniSuperMarket.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public string SubCategoryName { get; set; }

        [ForeignKey(nameof(MainCategory))]
        public int? MainCategoryId { get; set; }
        public MainCategory? MainCategories { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
