using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class CakesMetadata
    {
        [Display(Name = "喜餅編號")]
        public int CakeId { get; set; }
        [Display(Name = "商店編號")]
        public int ShopId { get; set; }
        [Display(Name = "喜餅風格")]
        public string? CakeStyles { get; set; }
        [Display(Name ="是否刪除")]
        public bool? IsDelete { get; set; }
        [Display(Name = "喜餅名稱")]
        public string? CakeName { get; set; }
        [Display(Name = "喜餅圖片")]
        public string? CakeImg { get; set; }
        [Display(Name = "喜餅描述")]
        public string? CakeDescription { get; set; }
        [Display(Name = "喜餅價格")]
        public int? CakePrice { get; set; }
        [Display(Name = "上架狀態")]
        public bool? CakeStatus { get; set; }
        [Display(Name = "喜餅庫存")]
        public int? CakeStock { get; set; }
        [Display(Name = "喜餅備註")]
        public string? CakeAnnotation { get; set; }
        [Display(Name = "過敏原資訊")]
        public string? AllergenInfo { get; set; }
        [Display(Name = "喜餅內容")]
        public string? CakeContent { get; set; }

    }
}