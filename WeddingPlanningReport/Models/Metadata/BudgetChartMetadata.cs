using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class BudgetChartMetadata
    {
        public int BudgetChartId { get; set; }

        [Display(Name = "會員")]
        [Required(ErrorMessage = "請確實填寫會員編號")]
        public int MemberId { get; set; }

        [Display(Name = "圖表類型")]
        public string? ChartType { get; set; }

        [Display(Name = "圖表名稱")]
        public string? ChartName { get; set; }

        [Display(Name = "圖表單位")]
        public string? ChartUnit { get; set; }
    }
}