using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class EditingImgMetadata
    {
        [Display(Name = "圖層ID")]
        [Required(ErrorMessage = "請確實選擇圖層ID")]
        public int EditingImgFileId { get; set; }

        [Display(Name = "會員ID")]
        [Required(ErrorMessage = "請確實選擇會員ID")]
        public int MemberId { get; set; }

        [Display(Name = "上次編輯時間")]
        public DateTime? EditTime { get; set; }

        [Display(Name = "縮圖")]
        public string? Screenshot { get; set; }

        [Display(Name = "圖層名稱")]
        [Required(ErrorMessage = "請確實填寫圖層名稱")]
        public string? ImgEditingName { get; set; }

        [Display(Name = "刪除狀態")]
        [Required(ErrorMessage = "請選擇刪除狀態")]
        public bool? IsDelete { get; set; }
    }
}