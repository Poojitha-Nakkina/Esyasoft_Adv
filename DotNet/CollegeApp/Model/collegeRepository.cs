namespace CollegeApp.Model
{
    public class collegeRepository
    {
        public static List<Student> students { get; set; } = new List<Student>(){ new Student
       {
           studentID = 1,
           name = "Test",
           age =20,
           email = "shivam@gmail.com"
       },

       new Student {

           studentID = 2,
           name = "Test",
           age = 20,
           email = "dsckjbdsc"
        }
       };
    }
}
