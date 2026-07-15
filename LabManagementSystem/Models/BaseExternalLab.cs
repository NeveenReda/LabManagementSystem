using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class BaseExternalLab
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<ExternalLab> ExternalLabs { get; set; } = new List<ExternalLab>();
}
