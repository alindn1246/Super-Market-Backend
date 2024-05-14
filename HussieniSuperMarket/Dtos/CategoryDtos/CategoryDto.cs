using HussieniSuperMarket.Dtos.SubCategoryDtos;
using HussieniSuperMarket.Models;

namespace HussieniSuperMarket.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? SubCategoryId { get; set; }

        public SubCategoryDto SubCategories { get; set; }

        
    }
}
