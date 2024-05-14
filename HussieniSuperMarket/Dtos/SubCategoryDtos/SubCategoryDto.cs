using HussieniSuperMarket.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HussieniSuperMarket.Dtos.MainCategoryDtos;

namespace HussieniSuperMarket.Dtos.SubCategoryDtos
{
    public class SubCategoryDto
    {
        
        public int Id { get; set; }

       

        public string SubCategoryName { get; set; }


        public int? MainCategoryId { get; set; }
        public MainCategoryDto? MainCategories { get; set; }






    }
}
