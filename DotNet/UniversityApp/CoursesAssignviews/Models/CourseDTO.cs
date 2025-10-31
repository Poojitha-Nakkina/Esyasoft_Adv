namespace CoursesAssignviews.Models
{
    public class CourseDTO
    {
        public int CourseId { get; set; }

        public string CourseCode { get; set; } = null!;

        public string CourseName { get; set; } = null!;

        public string? Department { get; set; }

        public int? Semester { get; set; }

    }
}
