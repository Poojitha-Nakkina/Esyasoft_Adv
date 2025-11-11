using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class TariffSlabDTO
    {
        public int SlabId { get; set; }

        [ForeignKey("TariffId")]
        public int TariffId { get; set; }

        public decimal FromKwh { get; set; }

        public decimal ToKwh { get; set; }

        public decimal RatePerKwh { get; set; }
    }
}
