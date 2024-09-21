using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class EventsMetadata
    {
        [Display(Name = "活動項目編號")]
        public int EventId { get; set; }

        [Display(Name = "婚禮計劃書編號")]
        [Required(ErrorMessage = "請確實填寫婚禮計劃書編號")]
        public int CaseId { get; set; }

        [Display(Name = "活動項目名稱")]
        [Required(ErrorMessage = "請確實填寫活動項目名稱")]
        public string? EventName { get; set; }

        [Display(Name = "活動開始時間")]
        public DateTime? EventTime { get; set; }

        [Display(Name = "活動地點")]
        public string? EventLocation { get; set; }

        [Display(Name = "活動備注")]
        public string? EventNotes { get; set; }

        [Display(Name = "活動地點圖片")]
        public string? EventLocationImg { get; set; }

        [Display(Name = "婚宴場地説明圖片")]
        public string? EventVenueImg1 { get; set; }

        [Display(Name = "婚宴場地説明圖片")]
        public string? EventVenueImg2 { get; set; }

        [Display(Name = "刪除狀態")]
        public bool? IsDelete { get; set; }
    }
}