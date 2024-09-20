using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class ImgUsing
{
    public int ImgUsingId { get; set; }

    public int EditingImgFileId { get; set; }

    public int? MemberMaterialId { get; set; }

    public int? MaterialId { get; set; }

    public decimal? ImgX { get; set; }

    public decimal? ImgY { get; set; }

    public string? ImgW { get; set; }

    public string? ImgH { get; set; }

    public virtual EditingImgFile EditingImgFile { get; set; } = null!;

    public virtual Material? Material { get; set; }

    public virtual MemberMaterial? MemberMaterial { get; set; }
}
