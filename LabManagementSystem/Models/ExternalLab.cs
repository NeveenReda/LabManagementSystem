using System;
using System.Collections.Generic;

namespace LabManagementSystem.Models;

public partial class ExternalLab
{
    public int Id { get; set; }

    public string ContactPerson { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? DateTimeSend { get; set; }

    public DateTime? DateTimeReceive { get; set; }

    public int VistLabId { get; set; }

    public int BaseExternalLabId { get; set; }

    public virtual BaseExternalLab BaseExternalLab { get; set; } = null!;

    public virtual VisitLab VistLab { get; set; } = null!;
}
