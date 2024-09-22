using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using WeddingPlanningReport.Models.ValidationAttributes;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class ComplaintReviewMetadata
    {
        [Display(Name = "檢舉事項ID")]
        public int ComplaintRecordId { get; set; }

        [Display(Name = "檢舉人ID")]
        public int ReporterMemberId { get; set; }

        [Display(Name = "婚禮企劃ID")]
        public int SharedRecordId { get; set; }

        [Display(Name = "檢舉時間")]
        public DateTime? ReportTime { get; set; }

        [Display(Name = "檢舉事由")]
        [ReadOnly(true)]  // 設為只讀
        public string? ReportDetail { get; set; }

        [Display(Name = "審核狀態", Prompt = "請選擇審核的狀態")]
        public string? ReviewStatus { get; set; }

        [Display(Name = "審核時間", Prompt = "請輸入審核完成的時間")]
        public DateTime? ReviewTime { get; set; }

        [Display(Name = "審核描述", Prompt = "請輸入審核結果的詳細說明")]
        public string? ReviewResultDescription { get; set; }

    }
}