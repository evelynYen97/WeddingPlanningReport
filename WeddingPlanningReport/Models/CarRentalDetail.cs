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

    public bool? IsDelete { get; set; }
}
