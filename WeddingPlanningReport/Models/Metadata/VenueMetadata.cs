using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class VenueMetadata
    {
        [Display(Name = "婚禮場地ID")]
        [Required(ErrorMessage = "請確實選擇婚禮場地ID")]
        public int VenueId { get; set; }

        [Display(Name = "廠商ID")]
        [Required(ErrorMessage = "請確實選擇廠商ID")]
        public int ShopId { get; set; }

        [Display(Name = "會員ID")]
        [Required(ErrorMessage = "請確實選擇會員ID")]
        public int? MemberId { get; set; }

        [Display(Name = "場地功能")]
        [Required(ErrorMessage = "請確實說明場地功能")]
        public string? VenueFunction { get; set; }

        [Display(Name = "場地風格")]
        [Required(ErrorMessage = "請確實說明場地風格")]
        public string? VenueStyle { get; set; }

        [Display(Name = "室內/室外")]
        [Required(ErrorMessage = "請確實說明室內/室外")]
        public string? InOurDoor { get; set; }

        [Display(Name = "婚禮場地名稱")]
        [Required(ErrorMessage = "請確實填寫婚禮場地名稱")]
        public string? VenueName { get; set; }

        [Display(Name = "場地容納桌數")]
        [Required(ErrorMessage = "請填寫場地容納桌數")]
        public int? TableCapacity { get; set; }

        [Display(Name = "場地容納人數")]
        [Required(ErrorMessage = "請填寫場地容納人數")]
        public int? GuestCapacity { get; set; }

        [Display(Name = "場地租金")]
        [Required(ErrorMessage = "請填寫場地租金")]
        public int? VenueRentalPrice { get; set; }

        [Display(Name = "場地圖片一")]
        public string? VenueImg { get; set; }

        [Display(Name = "場地資訊")]
        public string? VenueInfo { get; set; }

        [Display(Name = "開放預約時段")]
        [Required(ErrorMessage = "請填寫開放預約時段")]
        public DateTime? AvailableTime { get; set; }

        [Display(Name = "場地圖片二")]
        public string? VenueImg2 { get; set; }

        [Display(Name = "刪除狀態")]
        [Required(ErrorMessage = "請選擇刪除狀態")]
        public bool? IsDelete { get; set; }
    }
}