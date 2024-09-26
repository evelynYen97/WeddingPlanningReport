using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class CarRental
{
    public int RentalId { get; set; }

    public int MemberId { get; set; }

    public string? ClientName { get; set; }

    public string? ClientPhone { get; set; }

    public DateTime? LeaseDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? RentalTotal { get; set; }

    public string? RentalStatus { get; set; }
}
