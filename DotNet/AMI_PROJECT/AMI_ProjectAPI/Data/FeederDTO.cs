using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class FeederDTO
    {
        public int FeederId { get; set; }
        [ForeignKey("SubstationId")]
        public int SubstationId { get; set; }

        public string FeederName { get; set; } = null!;

    }
}
