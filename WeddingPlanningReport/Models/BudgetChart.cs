using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class BudgetChart
{
    public int BudgetChartId { get; set; }

    public int MemberId { get; set; }

    public string? ChartType { get; set; }

    public string? ChartName { get; set; }

    public string? ChartUnit { get; set; }
}
