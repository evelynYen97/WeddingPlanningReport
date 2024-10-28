using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int EventId { get; set; }

    public DateTime? ScheduleTime { get; set; }

    public string? ScheduleStageName { get; set; }

    public string? ScheduleStageNotes { get; set; }
}
