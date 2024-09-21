using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class ScheduleMetadata
    {
        [Display(Name = "排程ID")]
        [Required(ErrorMessage = "請確實填寫排程編號")]
        public int ScheduleId { get; set; }

        [Display(Name = "活動ID")]
        [Required(ErrorMessage = "請確實填寫活動編號")]
        public int EventId { get; set; }

        [Display(Name = "排程進行時間")]
        public TimeOnly? ScheduleTime { get; set; }

        [Display(Name = "排程名稱")]
        public string? ScheduleStageName { get; set; }

        [Display(Name = "排程備注")]
        public string? ScheduleStageNotes { get; set; }

        [Display(Name = "排程相關圖片")]
        public string? ScheduleStageImg1 { get; set; }

        [Display(Name = "刪除狀態")]
        public bool? IsDelete { get; set; }
    }
}