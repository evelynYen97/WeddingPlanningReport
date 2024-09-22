using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class CakerOrderMetadata
    {
        [Display(Name ="喜餅訂單編號")]
        public int CakeOrderId { get; set; }
        [Display(Name = "會員ID")]

        public int MemberId { get; set; }
        [Display(Name = "喜餅訂單總價")]

        public int? CakeOrderTotal { get; set; }
        [Display(Name = "訂單註記")]

        public string? CakeOrderAnnotation { get; set; }
        [Display(Name = "運送方式")]

        public string? Delivery { get; set; }
        [Display(Name = "付款方式")]

        public string? Payment { get; set; }
        [Display(Name = "訂單狀態")]

        public string? CakeOrderStatus { get; set; }

        [Display(Name = "是否刪除?")]
        public bool? IsDelete { get; set; }
    }
}