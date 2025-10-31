using CoursesAssignviews.Models;
using CoursesAssignviews.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoursesAssignviews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ICollegeRepository<Student> _studRepo;

        

      public StudentController(ICollegeRepository<Student> studRepo)
        {
            _studRepo = studRepo;
        }

        [HttpGet("AllStudents")]

        public async Task<ActionResult<List<Student>>> getAllStudents()
        {
            var values = await _studRepo.GetAllAsync();
            return values;
        }

        [HttpGet("stud/id/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var val = await _studRepo.GetAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("createStud")]
        public async Task<IActionResult> createStudent([FromBody] StudentDTO student)
        {
            var createdStud = await _studRepo.CreateAsync(new Student
            {
             StudentId  = student.StudentId,
                RollNumber= student.RollNumber,
                Name = student.Name,
                Email = student.Email,
                Phone= student.Phone,
                Address = student.Address,
                DateOfBirth= student.DateOfBirth,
                Gender= student.Gender,
                CourseId= student.CourseId,





            });
            return Ok(createdStud);

        }

        [HttpPut("UpdateStud/{id}")]
        public async Task<IActionResult> updateCourse(int id, [FromBody] StudentDTO student)
        {
            var val = await _studRepo.GetAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            val.RollNumber = student.RollNumber;
            val.Address = student.Address;
            val.Email = student.Email;
            val.Phone = student.Phone;
            val.Address = student.Address;
            val.DateOfBirth = student.DateOfBirth;
            val.Gender = student.Gender;
            val.CourseId = student.CourseId;
            var updated = await _studRepo.UpdateAsync(val);
            return Ok(updated);


        }

        [HttpDelete("Delete")]
        public async Task<bool> DeleteStud(int id)
        {
            var val = await _studRepo.GetAsync(id);
            if(val == null)
            {
                return false;
            }
            var deleted = await _studRepo.DeleteAsync(val);
            return true;
        }

        //[HttpPost("SendOtp")]
        //[AllowAnonymous]  // optional — remove if you want it to require authorization
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> SendOtp([FromBody] OtpRequestDto request)
        //{
        //    if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.name))
        //    {
        //        return BadRequest("Invalid input. Please provide name and email.");
        //    }

        //    // 1️⃣ Generate random 6-digit OTP
        //    string otp = _studRepo.Generaterandomnumber(); // if private, make it public or call through another method

        //    // 2️⃣ Send OTP email
        //    await _studRepo.SendOtpMail(request.Email, otp, request.name);

        //    // 3️⃣ Return success message (you can remove OTP in production)
        //    return Ok(new
        //    {
        //        Message = "OTP sent successfully to your email.",
        //        Otp = otp  // ⚠️ only for testing — don’t return OTP in real apps!
        //    });
        //}
    }
}
