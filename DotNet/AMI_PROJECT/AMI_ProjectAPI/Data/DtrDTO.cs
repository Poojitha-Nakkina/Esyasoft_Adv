using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class DtrDTO
    {
        public int Dtrid { get; set; }

        [ForeignKey("FeederId")]
        public int FeederId { get; set; }

        public string Dtrname { get; set; } = null!;
    }
}
