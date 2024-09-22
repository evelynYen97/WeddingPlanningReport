using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class ScheduledStaffMetadata
    {
        [Display(Name = "工作人員ID")]
        [Required(ErrorMessage = "請確實填寫工作人員編號")]
        public int PersonnelId { get; set; }

        [Display(Name = "排程")]
        [Required(ErrorMessage = "請確實填寫排程編號")]
        public int ScheduleId { get; set; }

        [Display(Name = "工作人員姓名")]
        [Required(ErrorMessage = "請確實填寫工作人員姓名")]
        public string? PersonnelName { get; set; }

        [Display(Name = "協助項目")]
        public string? AssistanceContent { get; set; }

        [Display(Name = "刪除狀態")]
        public bool? IsDelete { get; set; }
    }
}