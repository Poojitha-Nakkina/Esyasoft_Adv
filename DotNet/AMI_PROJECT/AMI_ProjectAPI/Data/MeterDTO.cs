using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class MeterDTO
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
    }
}
