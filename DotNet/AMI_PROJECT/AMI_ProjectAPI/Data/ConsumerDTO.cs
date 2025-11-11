using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class ConsumerDTO
    {
        public int ConsumerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        [ForeignKey("TariffId")]
        public int TariffId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
