using CoursesAssignviews.Models;
using CoursesAssignviews.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursesAssignviews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICollegeRepository<Course> _courseRepo;
        private readonly CollegeDbContext _dbContext;


        public CourseController(ICollegeRepository<Course> courseRepo, CollegeDbContext dbContext)
        {
            _courseRepo = courseRepo;
               _dbContext = dbContext;
        }

        [HttpGet("AllCourses")]

        public async Task<ActionResult<List<Course>>> getAllCourses()
        {
            var values = await _courseRepo.GetAllCourses();
            return values;
        }

        

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var val = await _courseRepo.GetAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("create")]

        public async Task<IActionResult> createCourse([FromBody] CourseDTO course)
        {
            var createdCourse = await _courseRepo.CreateAsync(new Course
            {
                CourseId = course.CourseId,
                CourseCode = course.CourseCode,
                CourseName = course.CourseName,
                Department = course.Department,
                Semester = course.Semester



            });
            return Ok(createdCourse);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> updateCourse(int id, [FromBody] CourseDTO course)
        {
            var val = await _courseRepo.GetAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            val.CourseName = course.CourseName;
            val.Department = course.Department;
            val.CourseCode = course.CourseCode;
            val.CourseCode = course.CourseCode;
            var updated = await _courseRepo.UpdateAsync(val);
            return Ok(updated);
            
            
        }

        [HttpDelete("DeleteCourse")]
        public async Task<bool> DeleteCourse(int id)
        {
            var val = await _courseRepo.GetAsync(id);
            if (val == null)
            {
                return false;
            }
            var studentCount = await _dbContext.Students.CountAsync(s => s.CourseId == id);
            string message = "Course deleted successfully.";
            if (studentCount > 0)
            {
                message = $"Students are enrolled in this course ({studentCount} student(s)). Deleting the course will also remove the associated students. Course and students deleted successfully.";
            }

            var deleted = await _courseRepo.DeleteAsync(val);
            return true;

       
        }

    }
}
