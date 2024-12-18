﻿using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class DishesOrderDetail
{
    public int DishesOrderDetailId { get; set; }

    public int DishesOrderId { get; set; }

    public int DishesId { get; set; }

    public int? DishesAmount { get; set; }

    public int? DishesSubtotal { get; set; }

    public int? MemberId { get; set; }

    public string? DishesName { get; set; }
}
