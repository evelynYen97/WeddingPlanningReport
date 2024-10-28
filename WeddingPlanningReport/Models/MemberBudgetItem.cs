using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class MemberBudgetItem
{
    public int BudgetItemId { get; set; }

    public int MemberId { get; set; }

    public string? BudgetItemDetail { get; set; }

    public int? BudgetItemPrice { get; set; }

    public int? BudgetItemAmount { get; set; }

    public int? BudgetItemSubtotal { get; set; }

    public string? BudgetItemSort { get; set; }

    public int? ActualPay { get; set; }

    public int? AlreadyPay { get; set; }
}
