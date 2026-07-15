using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class ProductUsage
{
    public int Id { get; set; }

    public string? Note { get; set; }

    public decimal ProductAmount { get; set; }

    public int? VisitId { get; set; }

    public int ProductId { get; set; }

    public int UnitId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;

    public virtual Visit? Visit { get; set; }
}
