using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class LabCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();
}
