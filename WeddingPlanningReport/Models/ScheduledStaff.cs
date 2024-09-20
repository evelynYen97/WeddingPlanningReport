using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class ScheduledStaff
{
    public int PersonnelId { get; set; }

    public int ScheduleId { get; set; }

    public string? PersonnelName { get; set; }

    public string? AssistanceContent { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;
}
