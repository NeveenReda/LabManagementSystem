using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialty { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
