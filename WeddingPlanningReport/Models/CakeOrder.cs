using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class CakeOrder
{
    public int CakeOrderId { get; set; }

    public int MemberId { get; set; }

    public int? CakeOrderTotal { get; set; }

    public string? CakeOrderAnnotation { get; set; }

    public string? Delivery { get; set; }

    public string? Payment { get; set; }

    public string? CakeOrderStatus { get; set; }

    public bool? IsDelete { get; set; }
}
