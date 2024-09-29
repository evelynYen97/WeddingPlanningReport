using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    public class DishesMetaData
    {
        [Display(Name = "廠商ID")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "菜單名稱未填寫")]
        [Display(Name = "菜單名稱")]
        [StringLength(30)]
        public string? DishesName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "租車日費")]
        public int? PricePerTable { get; set; }

        [Display(Name = "菜色描述")]
        public string? DishesDescription { get; set; }


        [Display(Name = "菜色圖片")]
        public string? DishesImg { get; set; }


        [Display(Name = "餐點類型")]
        public string? DishesSort { get; set; }



    }
}

