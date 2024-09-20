using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class EditingImgFile
{
    public int EditingImgFileId { get; set; }

    public int MemberId { get; set; }

    public DateTime? EditTime { get; set; }

    public string? Screenshot { get; set; }

    public string? ImgEditingName { get; set; }

    public virtual ICollection<ImgUsing> ImgUsings { get; set; } = new List<ImgUsing>();

    public virtual Member Member { get; set; } = null!;
}
