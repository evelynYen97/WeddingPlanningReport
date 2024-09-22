using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class SharingWeddingPlanMetadata
    {
        [Display(Name = "婚禮企劃ID")]
        //[ReadOnly(true)]
        public int CaseId { get; set; }


        [Display(Name = "發佈名稱")]
        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(100)]
        public string? SharedName { get; set; }


        [Display(Name = "發佈時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd tt hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? SharedTime { get; set; }


        [Display(Name = "發佈狀態")]
        public string? SharedStatus { get; set; }
    }
}