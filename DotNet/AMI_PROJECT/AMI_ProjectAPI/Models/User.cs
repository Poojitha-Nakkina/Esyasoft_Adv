using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Consumer|ZoneAdmin|SuperAdmin)$", ErrorMessage = "Invalid role type.")]
        public string Role { get; set; } = "Consumer";

        public string? DisplayName { get; set; }
        public string? Phone { get; set; }
        public bool Active { get; set; }


        public DateTime? LastLogin { get; set; }

        public string? ResetToken { get; set; }

        public DateTime? TokenExpiry { get; set; }

        [ForeignKey("ConsumerId")]
        public int? ConsumerId { get; set; }
        public Consumer? Consumer { get; set; }
    }
}
