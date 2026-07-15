using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class VisitLabResult
{
    public int Id { get; set; }

    public string ResultValue { get; set; } = null!;

    public string? Notes { get; set; }

    public int VisitLabId { get; set; }

    public int LabParameterId { get; set; }

    public virtual LabParameter LabParameter { get; set; } = null!;

    public virtual VisitLab VisitLab { get; set; } = null!;
}
