using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class VisitLabStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VisitLab> VisitLabs { get; set; } = new List<VisitLab>();
}
