using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class ReviewImage
{
    public int ReviewImageId { get; set; }

    public int? ShopReviewId { get; set; }

    public string? ImageName { get; set; }

    public byte[]? ImageUrl { get; set; }
}
