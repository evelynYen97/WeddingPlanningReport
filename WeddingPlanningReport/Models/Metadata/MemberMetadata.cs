using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class MemberMetadata
    {
        [Display(Name = "會員ID")]
        public int MemberId { get; set; }


        [Display(Name = "會員名稱", Prompt = "請輸入會員名稱")]
        //[Required(ErrorMessage = "請輸入會員名稱")]
        [StringLength(50, ErrorMessage = "會員名稱長度不能超過 50 個字元")]
        public string? MemberName { get; set; }


        [Display(Name = "電子郵件", Prompt = "請輸入電子郵件")]
        //[Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件")]
        public string? Email { get; set; }


        //[Display(Name = "密碼", Prompt = "請輸入密碼")]
        //[Required(ErrorMessage = "請輸入密碼")]
        //[StringLength(50, MinimumLength = 8, ErrorMessage = "密碼長度需在 8 到 50 個字元之間")]
        ////[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ////ErrorMessage = "密碼必須包含至少一個大寫字母、一個小寫字母、一個數字和一個特殊字元")]
        //public string? Password { get; set; }


        [Display(Name = "手機號碼", Prompt = "請輸入手機號碼")]
        //[RegularExpression(@"^09\d{8}$", ErrorMessage = "請輸入有效的手機號碼")]
        public string? PhoneNumber { get; set; }


        [Display(Name = "地址", Prompt = "請輸入地址")]
        public string? Address { get; set; }


        [Display(Name = "註冊時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd tt hh:mm:ss}", ApplyFormatInEditMode = false)]
        public DateTime? RegistrationTime { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd tt hh:mm:ss}", ApplyFormatInEditMode = false)]
        [Display(Name = "最後登入時間")]
        public DateTime? LastLoginTime { get; set; }


        [Display(Name = "會員等級", Prompt = "請輸入會員等級")]
        public string? MemberGrade { get; set; }


        [Display(Name = "會員狀態", Prompt = "請輸入會員狀態")]
        public string? MemberStatus { get; set; }


        [Display(Name = "性別", Prompt = "請選擇性別")]
        public string? Sex { get; set; }


        [Display(Name = "生日", Prompt = "請選擇生日")]
        [DataType(DataType.Date)]  // 使用 Date 而非 DateTime
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateOnly? Birthday { get; set; }

        [Display(Name = "偏好", Prompt = "請輸入偏好")]
        public string? Preference { get; set; }


        [Display(Name = "伴侶名稱", Prompt = "請輸入伴侶名稱")]
        public string? PartnerName { get; set; }


        [Display(Name = "婚禮狀態", Prompt = "請輸入婚禮狀態")]
        public string? WeddingStatus { get; set; }


        [Display(Name = "預算金額", Prompt = "請輸入預算金額")]
        public int? MemberBudget { get; set; }
    }
}