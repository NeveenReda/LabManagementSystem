using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Visit
{
    public int Id { get; set; }

    public DateTime VisitDate { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal? DiscountPercent { get; set; }

    public decimal? DiscountValue { get; set; }

    public decimal NetPrice { get; set; }

    public string? DrName { get; set; }

    public string? Notes { get; set; }

    public DateTime CreateDate { get; set; }

    public int PatientId { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ProductUsage> ProductUsages { get; set; } = new List<ProductUsage>();

    public virtual ICollection<VisitLab> VisitLabs { get; set; } = new List<VisitLab>();
}
