using System.ComponentModel.DataAnnotations;

namespace HussieniSuperMarket.Models
{
    public class MainCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MainCategoryName { get; set; }

        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
