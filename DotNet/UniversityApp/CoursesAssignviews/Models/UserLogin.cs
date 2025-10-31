using System.ComponentModel.DataAnnotations;

namespace CoursesAssignviews.Models
{
    public partial class UserLogin
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string Password { get; set; } = null!;

        public string? Role { get; set; }
        public string Email { get; set; }



    }
}
//
//[Key]
//public int Id { get; set; }
//[Required]
//public string Username { get; set; }
//[Required]
//public string Password { get; set; }  // WARNING: Hash this in production! Use BCrypt or similar.
//[Required]
//public string Role { get; set; }