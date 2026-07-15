using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class LabParameterCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int LabId { get; set; }

    public virtual Lab Lab { get; set; } = null!;

    public virtual ICollection<LabParameter> LabParameters { get; set; } = new List<LabParameter>();
}
