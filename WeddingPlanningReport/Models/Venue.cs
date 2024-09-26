using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    public int ShopId { get; set; }

    public int? MemberId { get; set; }

    public string? VenueFunction { get; set; }

    public string? VenueStyle { get; set; }

    public string? InOurDoor { get; set; }

    public string? VenueName { get; set; }

    public int? TableCapacity { get; set; }

    public int? GuestCapacity { get; set; }

    public int? VenueRentalPrice { get; set; }

    public string? VenueImg { get; set; }

    public string? VenueInfo { get; set; }

    public DateTime? AvailableTime { get; set; }

    public string? VenueImg2 { get; set; }

    public bool IsDelete { get; set; }
}
