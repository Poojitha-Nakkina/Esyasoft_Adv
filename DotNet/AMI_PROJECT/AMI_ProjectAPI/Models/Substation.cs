using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Models;

public partial class Substation
{
    public int SubstationId { get; set; }

    [ForeignKey("ZoneId")]
    public int ZoneId { get; set; }

    public string SubstationName { get; set; } = null!;

    public virtual ICollection<Feeder> Feeders { get; set; } = new List<Feeder>();

    public virtual Zone Zone { get; set; } = null!;
}
