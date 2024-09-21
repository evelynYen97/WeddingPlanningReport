using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models
{
    internal class MemberBudgetItemMetadata
    {
        [Display(Name = "預算項目ID")]
        [Required(ErrorMessage = "請確實填寫婚禮計劃書編號")]
        public int BudgetItemId { get; set; }

        [Display(Name = "會員")]
        [Required(ErrorMessage = "請確實填寫會員編號")]
        public int MemberId { get; set; }

        [Display(Name = "預算項目説明")]
        [Required(ErrorMessage = "請確實填寫預算項目標題或描述")]
        public string? BudgetItemDetail { get; set; }

        [Display(Name = "項目單價")]
        public int? BudgetItemPrice { get; set; }

        [Display(Name = "項目數量")]
        public int? BudgetItemAmount { get; set; }

        [Display(Name = "項目小計")]
        public int? BudgetItemSubtotal { get; set; }

        [Display(Name = "項目分類")]
        public string? BudgetItemSort { get; set; }
    }
}