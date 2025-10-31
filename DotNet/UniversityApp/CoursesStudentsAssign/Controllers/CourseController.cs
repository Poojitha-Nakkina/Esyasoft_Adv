using Microsoft.AspNetCore.Mvc;

namespace CoursesStudentsAssign.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Courses()
        {
            return View();
        }

       
        public IActionResult Add()
        {
            return View();
        }
       
        public IActionResult Edit(int id)
        {
            ViewBag.CourseId = id;
            return View();
        }
     
        public IActionResult Delete(int id)
        {
            ViewBag.CourseId = id;
            return View();
        }
    }
}
