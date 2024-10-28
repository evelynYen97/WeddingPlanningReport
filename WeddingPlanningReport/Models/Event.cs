using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int CaseId { get; set; }

    public string? EventName { get; set; }

    public DateTime? EventTime { get; set; }

    public string? EventLocation { get; set; }

    public string? EventNotes { get; set; }

    public string? EventLocationImg { get; set; }
}
