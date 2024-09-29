using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class DishesOrder
{
    public int DishesOrderId { get; set; }

    public int MemberId { get; set; }

    public string? DishesOrderName { get; set; }

    public DateTime? DishesSupplyDate { get; set; }

    public int? DishesTotalPrice { get; set; }

    public DateTime? OrderTime { get; set; }
}
