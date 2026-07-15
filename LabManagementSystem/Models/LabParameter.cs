using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class LabParameter
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Unit { get; set; }

    public bool IsNumeric { get; set; }

    public decimal? NormalRangeMin { get; set; }

    public decimal? NormalRangeMax { get; set; }

    public string? NormalText { get; set; }

    public int DisplayOrder { get; set; }

    public int LabParametersCategoryId { get; set; }

    public int? GenderId { get; set; }

    public int? LabId { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual Lab? Lab { get; set; }

    public virtual LabParameterCategory LabParametersCategory { get; set; } = null!;

    public virtual ICollection<VisitLabResult> VisitLabResults { get; set; } = new List<VisitLabResult>();
}
