using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class ShopReview
{
    public int ShopReviewId { get; set; }

    public int? ShopId { get; set; }

    public int? MemberId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public bool? OrderYet { get; set; }

    public DateTime? CreatedTime { get; set; }

    public bool? Status { get; set; }
}
