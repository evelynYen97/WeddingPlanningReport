using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class CakeOrderDetailsMetadata
    {
        [Display(Name ="喜餅明細ID")]
        public int CakeOrderDetailId { get; set; }
        [Display(Name = "喜餅編號")]

        public int CakeId { get; set; }
        [Display(Name = "喜餅訂單編號")]

        public int CakeOrderId { get; set; }
        [Display(Name = "喜餅價格")]

        public int? CakePrice { get; set; }
        [Display(Name = "喜餅數量")]

        public int? CakeAmount { get; set; }
        [Display(Name = "喜餅總價")]

        public int? CakeSubtotal { get; set; }
    }
}