using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class BillReadingDTO
    {
        public int BillId { get; set; }
        [ForeignKey("ConsumerId")]
        public int ConsumerId { get; set; }
        [ForeignKey("MeterId")]

        public int MeterId { get; set; }
        [ForeignKey("TariffId")]

        public int TariffId { get; set; }

        public string BillingMonth { get; set; } = null!;

        public decimal TotalConsumption { get; set; }

        public string TariffType { get; set; } = null!;

        public decimal EnergyCharge { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalBill { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? GeneratedDate { get; set; }
    }
}
