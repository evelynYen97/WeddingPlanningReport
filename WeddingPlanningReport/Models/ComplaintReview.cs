using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class ComplaintReview
{
    public int ComplaintRecordId { get; set; }

    public int ReporterMemberId { get; set; }

    public int RespondentMemberId { get; set; }

    public DateTime? ReportTime { get; set; }

    public string? ReportDetail { get; set; }

    public string? ReviewStatus { get; set; }

    public DateTime? ReviewTime { get; set; }

    public int? ReviewerId { get; set; }

    public string? ReviewerName { get; set; }

    public string? ReviewNotes { get; set; }

    public string? ReviewResultDescription { get; set; }
}
