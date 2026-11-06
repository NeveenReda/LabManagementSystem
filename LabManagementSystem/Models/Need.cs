using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Need
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string Notes { get; set; } = null!;

    public bool IsCompleted { get; set; } = false;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? DueDate { get; set; }

    public int Priority { get; set; } = 2; // 1=Low, 2=Medium, 3=High, 4=Urgent

    public bool IsDeleted { get; set; } = false;
}
