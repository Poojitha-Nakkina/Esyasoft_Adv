using Microsoft.AspNetCore.Mvc;

namespace CoursesStudentsAssign.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Students()
        {
            return View();
        }

        // New: Add Student page
        public IActionResult Add()
        {
            return View();
        }
        // New: Edit Student page (pass studentId via route)
        public IActionResult Edit(int id)
        {
            ViewBag.StudentId = id;  // Pass ID to the view
            return View();
        }
        // New: Delete Student page (pass studentId via route)
        public IActionResult Delete(int id)
        {
            ViewBag.StudentId = id;  // Pass ID to the view
            return View();
        }
    }
}
