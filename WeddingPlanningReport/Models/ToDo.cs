using System;
using System.Collections.Generic;

namespace WeddingPlanningReport.Models;

public partial class ToDo
{
    public int ToDoId { get; set; }

    public int MemberId { get; set; }

    public string? ToDoName { get; set; }

    public string? ToDoDetail { get; set; }

    public DateTime? ExpiringDate { get; set; }

    public int? Precedence { get; set; }

    public string? TaskStatus { get; set; }

    public string? PersonInCharge { get; set; }

    public string? TaskSort { get; set; }

    public DateTime? RemindTime { get; set; }

    public string? AdditionalData { get; set; }

    public DateTime? CreateToDoTime { get; set; }

    public DateTime? UpdateToDoTime { get; set; }

    public bool? RepetitiveTask { get; set; }
}
