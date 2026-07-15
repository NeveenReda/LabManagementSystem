using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
