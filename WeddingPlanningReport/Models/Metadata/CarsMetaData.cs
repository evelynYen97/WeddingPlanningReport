using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class CarsMetaData
    {
        [Display(Name = "廠商ID")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "商品名稱未填寫")]
        [Display(Name = "商品名稱")]
        [StringLength(30)]
        public string? CarName { get; set; }

        [Display(Name = "乘坐數量")]
        public int? PassengerCapacity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "每日租費")]
        public int? RentalPerDay { get; set; }

        [Display(Name = "車況")]
        public string? CarStatus { get; set; }


        [Display(Name = "婚車圖片")]
        public string? CarImg { get; set; }

        [Display(Name = "描述")]
        public string? CarDetail { get; set; }

        [Display(Name = "車子數量")]
        public int? quantity { get; set; }


    }
}