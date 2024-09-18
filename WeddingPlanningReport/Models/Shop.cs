using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string? ShopName { get; set; }

    public string? ShopSort { get; set; }

    public string? ContactPerson { get; set; }

    public string? ContactPhone { get; set; }

    public decimal? ShopRating { get; set; }

    public string? ShopLogo { get; set; }

    public string? ShopImg { get; set; }

    public string? Payment { get; set; }

    public string? ServiceArea { get; set; }

    public int? ShopStatus { get; set; }
}
