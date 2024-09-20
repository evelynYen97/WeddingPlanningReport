using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class SharingWeddingPlan
{
    public int SharedRecordId { get; set; }

    public int CaseId { get; set; }

    public DateTime? SharedTime { get; set; }

    public string? SharedName { get; set; }

    public string? SharedStatus { get; set; }

    public bool? IsDelete { get; set; }
}
