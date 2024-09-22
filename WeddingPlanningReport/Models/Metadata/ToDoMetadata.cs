using System.ComponentModel.DataAnnotations;

namespace WeddingPlanningReport.Models.Metadata
{
    internal class ToDoMetadata
    {
        [Display(Name = "待辦事項ID")]
        public int ToDoId { get; set; }

        [Display(Name = "會員ID")]
        public int MemberId { get; set; }

        [Display(Name = "待辦事項名稱")]
        public string? ToDoName { get; set; }

        [Display(Name = "事項詳情")]
        public string? ToDoDetail { get; set; }

        [Display(Name = "到期日期")]
        public DateTime? ExpiringDate { get; set; }

        [Display(Name = "事項狀態")]
        public string? TaskStatus { get; set; }

        [Display(Name = "負責人")]
        public string? PersonInCharge { get; set; }

        [Display(Name = "事項分類")]
        public string? TaskSort { get; set; }



    


    

       
    }
}