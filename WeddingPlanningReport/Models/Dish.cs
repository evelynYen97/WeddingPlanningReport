using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Dish
{
    public int DishesId { get; set; }

    public int ShopId { get; set; }

    public string? DishesName { get; set; }

    public int? PricePerTable { get; set; }

    public string? DishesDescription { get; set; }

    public string? DishesImg { get; set; }

    public string? DishesSort { get; set; }

    public bool? IsDelete { get; set; }
}
