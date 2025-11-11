using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class MeterReadingDTO
    {
        public int ReadingId { get; set; }
        [ForeignKey("MeterId")]

        public int MeterId { get; set; }

        public decimal ReadingValue { get; set; }

        public DateTime ReadingDate { get; set; }

        public DateTime CreatedAt { get; set; }

        
    }
}
