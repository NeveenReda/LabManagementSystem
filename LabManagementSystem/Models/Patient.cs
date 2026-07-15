using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string MedicalCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int AgeAtRecord { get; set; }

    public string? Address { get; set; }

    public DateTime CreateDate { get; set; }

    public int GenderId { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
