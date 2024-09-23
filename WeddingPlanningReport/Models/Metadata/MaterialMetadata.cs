using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class MaterialMetadata
    {
        [Display(Name = "素材ID")]
        [Required(ErrorMessage = "請確實填寫素材ID")]
        public int MaterialId { get; set; }

        [Display(Name = "素材名稱")]
        public string? ImageName { get; set; }

        [Display(Name = "長度(cm)")]
        [Required(ErrorMessage = "請確實填寫素材長度")]
        public int? EstimatedL { get; set; }

        [Display(Name = "寬度(cm)")]
        [Required(ErrorMessage = "請確實填寫素材寬度")]
        public int? EstimatedW { get; set; }
    }
}