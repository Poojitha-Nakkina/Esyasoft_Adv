using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Models;

public partial class MeterReading
{
    public int ReadingId { get; set; }
    [ForeignKey("MeterId")]

    public int MeterId { get; set; }

    public decimal ReadingValue { get; set; }

    public DateTime ReadingDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Meter Meter { get; set; } = null!;
}
