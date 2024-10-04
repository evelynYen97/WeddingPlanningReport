using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.ViewModel
{
    public class MailViewModel
    {
        public int MailId { get; set; }

        [Display(Name = "會員名稱")]
        public string? MemberName { get; set; }

        [Display(Name = "會員郵件")]
        public string? MemberEmail { get; set; }

        [Display(Name = "郵件時間")]
        public DateTime? MailDate { get; set; }

        [Display(Name = "郵件標題")]
        public string? MailTitle { get; set; }

        [Display(Name = "郵件內容")]
        public string? MailContent { get; set; }

        [Required(ErrorMessage = "回覆內容不能為空。")]
        [StringLength(500, ErrorMessage = "回覆內容不能超過 500 個字元。")]
        [Display(Name = "回覆內容")]
        public string? ReplyContent { get; set; }

        [Display(Name = "回覆時間")]
        public DateTime? ReplyDate { get; set; }

        [Display(Name = "回覆狀態")]
        public string IsReplied { get; set; } = "未回覆"; 
    }
}
