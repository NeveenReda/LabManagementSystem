using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Need
{
    public int Id { get; set; }

    public string Notes { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
