using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class WeddingPlanMetadata
    {
        
        public int CaseId { get; set; }

        [Display(Name = "會員")]
        [Required(ErrorMessage = "請確實填寫會員ID")]
        public int MemberId { get; set; }

        [Display(Name = "婚禮計劃書名稱")]
        [Required(ErrorMessage = "婚禮計劃書名稱必填")]
        public string? WeddingName { get; set; }

        [Display(Name = "婚禮雙方介紹")]
        public string? Introduction { get; set; }

        [Display(Name = "婚禮時間")]
        public DateTime? WeddingTime { get; set; }

        [Display(Name = "婚禮地點")]
        public string? WeddingLocation { get; set; }

        [Display(Name = "刪除狀態")]
        public bool? IsDelete { get; set; }
    }
}