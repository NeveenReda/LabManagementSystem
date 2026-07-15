using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class VisitLab
{
    public int Id { get; set; }

    public decimal LabPrice { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public bool IsExternal { get; set; }

    public string? Notes { get; set; }

    public int VisitLabStatusId { get; set; }

    public int VisitId { get; set; }

    public int LabId { get; set; }

    public virtual ICollection<ExternalLab> ExternalLabs { get; set; } = new List<ExternalLab>();

    public virtual Lab Lab { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;

    public virtual ICollection<VisitLabResult> VisitLabResults { get; set; } = new List<VisitLabResult>();

    public virtual VisitLabStatus VisitLabStatus { get; set; } = null!;
}
