using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class HomePage
{
    public int HomePageId { get; set; }

    public string? LogoImg { get; set; }

    public string? ContentName { get; set; }

    public string? ContentBody { get; set; }

    public string? ContentLink { get; set; }

    public string? ContentImg { get; set; }

    public string? ContentVideo { get; set; }

    public string? CommunityLinkText { get; set; }

    public string? CommunityLink { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? CreationCreatTime { get; set; }
}
