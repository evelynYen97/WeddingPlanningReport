using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Cars
{
    public int CarId { get; set; }

    public int ShopId { get; set; }

    public string? CarName { get; set; }

    public int? PassengerCapacity { get; set; }

    public int? RentalPerDay { get; set; }    

    public int? quantity { get; set; }

    public string? CarStatus { get; set; }

    public string? CarImg { get; set; }

    public string? CarDetail { get; set; }

    public bool? IsDelete { get; set; }

}
