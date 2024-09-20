using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class WeddingPlan
{
    public int CaseId { get; set; }

    public int MemberId { get; set; }

    public string? WeddingName { get; set; }

    public string? Introduction { get; set; }

    public DateTime? WeddingTime { get; set; }

    public string? WeddingLocation { get; set; }

    public bool? IsDelete { get; set; }
}
