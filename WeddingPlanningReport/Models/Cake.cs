using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Cake
{
    public int CakeId { get; set; }

    public int ShopId { get; set; }

    public string? CakeStyles { get; set; }

    public string? CakeName { get; set; }

    public string? CakeImg { get; set; }

    public string? CakeDescription { get; set; }

    public int? CakePrice { get; set; }

    public bool? CakeStatus { get; set; }

    public int? CakeStock { get; set; }

    public string? CakeAnnotation { get; set; }

    public string? AllergenInfo { get; set; }

    public string? CakeContent { get; set; }

    
 
}
