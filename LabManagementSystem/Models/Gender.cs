using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class Gender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<LabParameter> LabParameters { get; set; } = new List<LabParameter>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
