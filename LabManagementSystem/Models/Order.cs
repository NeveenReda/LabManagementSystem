using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? ExpectedDeliveryDate { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal NetPrice { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
