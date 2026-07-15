using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MinAmount { get; set; }

    public int InitialAmount { get; set; }

    public DateTime CreateDate { get; set; }

    public int BaseUnitId { get; set; }

    public int ProductCategoryId { get; set; }

    public virtual Unit BaseUnit { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual ICollection<ProductUnit> ProductUnits { get; set; } = new List<ProductUnit>();

    public virtual ICollection<ProductUsage> ProductUsages { get; set; } = new List<ProductUsage>();
}
