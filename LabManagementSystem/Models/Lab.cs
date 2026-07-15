using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Lab
{
    public int Id { get; set; }

    public int LabCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Days { get; set; }

    public bool IsExternal { get; set; }

    public virtual LabCategory LabCategory { get; set; } = null!;

    public virtual ICollection<LabParameterCategory> LabParameterCategories { get; set; } = new List<LabParameterCategory>();

    public virtual ICollection<LabParameter> LabParameters { get; set; } = new List<LabParameter>();

    public virtual ICollection<VisitLab> VisitLabs { get; set; } = new List<VisitLab>();
}
