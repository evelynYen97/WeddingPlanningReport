using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class CarRentalDetail
{
    public int RentalDetailId { get; set; }

    public int RentalId { get; set; }

    public int CarId { get; set; }

    public int? LeaseDays { get; set; }

    public int? LeaseSubtotal { get; set; }

    public int? Quantity { get; set; }

    public int? MemberId { get; set; }

    public string? CarName { get; set; }
}
