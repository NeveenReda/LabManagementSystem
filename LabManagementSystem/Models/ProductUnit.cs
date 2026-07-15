using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class ProductUnit
{
    public int Id { get; set; }

    public decimal ConversionToBase { get; set; }

    public bool IsBaseUnit { get; set; }

    public int ProductId { get; set; }

    public int UnitId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
