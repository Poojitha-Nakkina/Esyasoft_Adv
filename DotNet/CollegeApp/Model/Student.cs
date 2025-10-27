using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Model
{
    public class Student
    {
        [Required]
        public int studentID { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string email { get; set; }
    }
}
