using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int EventId { get; set; }

    public TimeOnly? ScheduleTime { get; set; }

    public string? ScheduleStageName { get; set; }

    public string? ScheduleStageNotes { get; set; }

    public string? ScheduleStageImg1 { get; set; }

    public bool? IsDelete { get; set; }
}
