using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class CakeOrderDetail
{
    public int CakeOrderDetailId { get; set; }

    public int CakeId { get; set; }

    public int CakeOrderId { get; set; }

    public int? CakePrice { get; set; }

    public int? CakeAmount { get; set; }

    public int? CakeSubtotal { get; set; }
}
