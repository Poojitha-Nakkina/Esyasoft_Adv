using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Models;

public partial class Feeder
{
    public int FeederId { get; set; }
    [ForeignKey("SubstationId")]
    public int SubstationId { get; set; }

    public string FeederName { get; set; } = null!;

    public virtual ICollection<Dtr> Dtrs { get; set; } = new List<Dtr>();

    public virtual Substation Substation { get; set; } = null!;
}
