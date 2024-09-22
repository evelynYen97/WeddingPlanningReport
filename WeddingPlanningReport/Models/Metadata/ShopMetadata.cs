using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class ShopMetadata
    {
        [Display(Name ="商家編號")]
        public int ShopId { get; set; }
        [Display(Name = "商家名稱")]
        public string? ShopName { get; set; }
        [Display(Name = "服務項目")]
        public string? ShopSort { get; set; }
        [Display(Name = "商家聯絡人")]
        public string? ContactPerson { get; set; }
        [Display(Name = "商家連絡電話")]
        public string? ContactPhone { get; set; }
        [Display(Name = "商家評價(5星)")]
        public decimal? ShopRating { get; set; }
        [Display(Name = "商家Logo")]
        public string? ShopLogo { get; set; }
        [Display(Name = "商家圖案")]
        public string? ShopImg { get; set; }
        [Display(Name = "付款方式")]
        public string? Payment { get; set; }
        [Display(Name = "服務區域")]
        public string? ServiceArea { get; set; }
        [Display(Name = "商家狀態")]
        public int? ShopStatus { get; set; }
        [Display(Name = "是否刪除?")]
        public bool? IsDelete { get; set; }
    }
}