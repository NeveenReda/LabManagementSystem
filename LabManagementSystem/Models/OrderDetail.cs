using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string? Notes { get; set; }

    public DateTime CreateDate { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int UnitId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
