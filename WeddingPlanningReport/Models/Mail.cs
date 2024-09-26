using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Mail
{
    public int MailId { get; set; }

    public int? MemberId { get; set; }

    public string? MemberName { get; set; }

    public string? MemberEmail { get; set; }

    public DateTime? MailDate { get; set; }

    public string? MailTitle { get; set; }

    public string? MailContent { get; set; }

    public DateTime? ReplyDate { get; set; }

    public string? ReplyContent { get; set; }

    public string IsReplied { get; set; } = null!;
}
