using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public string? ImageName { get; set; }

    public int? EstimatedL { get; set; }

    public int? EstimatedW { get; set; }

    public virtual ICollection<ImgUsing> ImgUsings { get; set; } = new List<ImgUsing>();
}
