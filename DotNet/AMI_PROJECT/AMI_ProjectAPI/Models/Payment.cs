using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("BillReading")]
        public int BillId { get; set; }

        [ForeignKey("Consumer")]
        public int ConsumerId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AmountPaid { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal RemainingBalance { get; set; } // can go negative (advance)

        [MaxLength(50)]
        public string? PaymentMethod { get; set; } // e.g., Cash, Card, UPI, NetBanking

        [MaxLength(100)]
        public string? ReferenceNumber { get; set; } // Transaction ID / UPI ref

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [MaxLength(20)]
        public string Status { get; set; } = "Completed"; // Completed / Failed / Pending

        // 🔗 Navigation Properties
        public BillReading? BillReading { get; set; }
        public Consumer? Consumer { get; set; }

    }
}
