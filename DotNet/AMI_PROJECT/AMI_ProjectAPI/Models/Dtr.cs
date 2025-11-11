using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AMI_ProjectAPI.Models;

public partial class Dtr
{
    public int Dtrid { get; set; }
    [ForeignKey("FeederId")]
    public int FeederId { get; set; }

    public string Dtrname { get; set; } = null!;

    public virtual Feeder Feeder { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Meter> Meters { get; set; } = new List<Meter>();
}
