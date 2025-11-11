using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class SubstationDTO
    {
        public int SubstationId { get; set; }
        [ForeignKey("ZoneId")]
        public int ZoneId { get; set; }

        public string SubstationName { get; set; } = null!;
    }
}
