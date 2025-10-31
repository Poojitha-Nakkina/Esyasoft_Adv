using System;
using System.Collections.Generic;

namespace CoursesAssignviews.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string Password { get; set; } = null!;

        public string? Role { get; set; }
    }
}



