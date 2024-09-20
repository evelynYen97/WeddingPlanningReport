using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? MemberName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? RegistrationTime { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public string? MemberGrade { get; set; }

    public string? MemberStatus { get; set; }

    public string? Sex { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Preference { get; set; }

    public string? Notes { get; set; }

    public bool? VerifyByPhone { get; set; }

    public string? PartnerName { get; set; }

    public string? WeddingStatus { get; set; }

    public string? BudgetPieChart { get; set; }

    public string? BudgetTableImg { get; set; }

    public bool? IsDelete { get; set; }
}
