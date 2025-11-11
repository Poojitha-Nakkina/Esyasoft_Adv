using System;
using System.Collections.Generic;

namespace AMI_ProjectAPI.Models;

public partial class Zone
{
    public int ZoneId { get; set; }

    public string ZoneName { get; set; } = null!;

    public virtual ICollection<Substation> Substations { get; set; } = new List<Substation>();
}
