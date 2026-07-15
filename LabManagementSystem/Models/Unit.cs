using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductUnit> ProductUnits { get; set; } = new List<ProductUnit>();

    public virtual ICollection<ProductUsage> ProductUsages { get; set; } = new List<ProductUsage>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
