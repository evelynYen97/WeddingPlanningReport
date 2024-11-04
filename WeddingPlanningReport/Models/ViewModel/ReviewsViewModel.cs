using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.ViewModel
{
    public class ReviewsViewModel
    {
        public int ShopReviewId { get; set; }
        [Display(Name = "商家名稱")]
        public string ShopName { get; set; }

        public int? ShopId { get; set; }

        public int? MemberId { get; set; }
        [Display(Name = "會員名稱")]
        public string MemberName { get; set; }

        [Display(Name = "星星評分")]

        public int? Rating { get; set; }

        [Display(Name = "評價")]

        public string? Comment { get; set; }
        [Display(Name = "是否曾在婚禮訂購")]

        public bool? OrderYet { get; set; }

        public string OrderYetText
        {
            get
            {
                return OrderYet.HasValue
                ? (OrderYet.Value ? "是" : "否")
                : "否";
            }
        }
        [Display(Name = "評價時間")]

        public DateTime? CreatedTime { get; set; }
    }
}
