using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesAssignviews.Models
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        public string RollNumber { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
    }
}
