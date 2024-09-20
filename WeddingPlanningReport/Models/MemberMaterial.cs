using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class MemberMaterial
{
    public int MemberMaterialId { get; set; }

    public int MemberId { get; set; }

    public string? MemberImgName { get; set; }

    public int? EstimatedLength { get; set; }

    public int? EstimatedWidth { get; set; }

    public virtual ICollection<ImgUsing> ImgUsings { get; set; } = new List<ImgUsing>();
}
