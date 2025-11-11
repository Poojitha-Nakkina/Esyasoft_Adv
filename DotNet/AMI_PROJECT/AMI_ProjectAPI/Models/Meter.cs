using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Models;

public partial class Meter
{
    public int MeterId { get; set; }

    public string MeterSerialNo { get; set; } = null!;

    [ForeignKey("ConsumerId")]
    public int? ConsumerId { get; set; }

    [ForeignKey("Dtrid")]
    public int? Dtrid { get; set; }

    public string Ipaddress { get; set; } = null!;

    public string Iccid { get; set; } = null!;

    public string Imsi { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string? Firmware { get; set; }

    public string Category { get; set; } = null!;

    public DateTime InstallDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<BillReading> BillReadings { get; set; } = new List<BillReading>();

    public virtual Consumer? Consumer { get; set; }

    public virtual Dtr? Dtr { get; set; }

    public virtual ICollection<MeterReading> MeterReadings { get; set; } = new List<MeterReading>();
}
