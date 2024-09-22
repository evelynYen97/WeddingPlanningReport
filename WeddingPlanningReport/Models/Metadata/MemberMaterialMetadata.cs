using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class MemberMaterialMetadata
    {
        [Display(Name = "會員素材ID")]
        [Required(ErrorMessage = "請確實填寫會員素材ID")]
        public int MemberMaterialId { get; set; }

        [Display(Name = "會員ID")]
        [Required(ErrorMessage = "請確實填寫會員ID")]
        public int MemberId { get; set; }

        [Display(Name = "提供素材名稱")]
        [Required(ErrorMessage = "請確實填寫提供素材名稱")]
        public string? MemberImgName { get; set; }

        [Display(Name = "素材長度")]
        [Required(ErrorMessage = "請確實填寫素材長度")]
        public int? EstimatedLength { get; set; }

        [Display(Name = "素材寬度")]
        [Required(ErrorMessage = "請確實填寫素材寬度")]
        public int? EstimatedWidth { get; set; }

        [Display(Name = "刪除狀態")]
        [Required(ErrorMessage = "請選擇刪除狀態")]
        public bool? IsDelete { get; set; }
    }
}