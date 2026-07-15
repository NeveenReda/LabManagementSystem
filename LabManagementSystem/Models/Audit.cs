using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Audit
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Details { get; set; }

    public DateTime Timestamp { get; set; }

    public int Operation { get; set; }

    public int TableRecordId { get; set; }

    public int TableName { get; set; }

    public string? MachineName { get; set; }

    public virtual User User { get; set; } = null!;
}
