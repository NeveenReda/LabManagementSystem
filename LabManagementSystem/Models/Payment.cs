using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Payment
{
    public int Id { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public int VisitId { get; set; }

    public virtual Visit Visit { get; set; } = null!;
}
