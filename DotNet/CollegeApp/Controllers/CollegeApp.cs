
using CollegeApp.Model;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;




namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeApp : ControllerBase
    {
        //[HttpGet]
        //[Route("All")]
        //public IEnumerable<Student> getstudents()
        //{
        //    return collegeRepository.students;
        //}

        //[HttpGet]
        //[Route("getstudentById")]
        //[HttpGet("id/{id}",Name = "getstudentById")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public ActionResult<Student> getstudentById(int id)
        //{
        //    if(id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var stud =  collegeRepository.students.Where(std => std.studentID == id).FirstOrDefault();
        //    if (stud == null)
        //    {
        //        return NotFound("No student found with the given ID");
        //    }
        //    return stud;
        //}

        //[HttpGet]
        //[Route("getStudentByName")]







        [HttpGet("name/{Name}",Name = "getStudentByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Student>> getStudentByName(string Name)
        {
            if(String.IsNullOrEmpty(Name))
            {
                return BadRequest();
            }
            var student = collegeRepository.students.Where(std => std.name == Name);
            if(!student.Any())
            {
                return NotFound("No student found with given Name");
            }
            
            
            return Ok(student);
            
        }

        [HttpDelete]

        public bool DeleteStudentById(int id)
        {
            var stude = collegeRepository.students.Where(std => std.studentID == id).FirstOrDefault();
            collegeRepository.students.Remove(stude);
            return true;
        }

        [HttpGet]
        [Route("All")]

        public ActionResult<IEnumerable<studentDTO>> getStudents()
        {
            var students = collegeRepository.students.Select(s => new studentDTO()
            {
                //studentID = s.studentID,
                name = s.name,
                age = s.age,
                email = s.email
            });
            return Ok(students);
        }

        [HttpGet]
        [HttpGet("id/{id}", Name = "getstudentById")]

        public ActionResult<IEnumerable<studentDTO>> getStudentsById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var stud = collegeRepository.students.Where(std => std.studentID == id).Select(s => new studentDTO()
            {
                //studentID = s.studentID,
                name = s.name,
                age = s.age,
                email = s.email
            });

            if (!stud.Any())
            {
                return NotFound("No student found with given Id");
            }
            return Ok(stud);
        }

        [HttpPost("Create")]
        

        public ActionResult<studentDTO> CreateStudent([FromBody]studentDTO Model)
        {
            if(Model == null)
            {
                return BadRequest();
            }

            int newid = collegeRepository.students.LastOrDefault().studentID + 1;

            Student studentnew = new Student
            {
                studentID = newid,
                name = Model.name,
                age =Model.age,
                email = Model.email
            };

            collegeRepository.students.Add(studentnew);

            return Ok(Model);
            
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateStudent(int id, [FromBody] studentDTO Model)
        {
            if(Model == null || id == 0)
            {
                return BadRequest();
            }
            var stud = collegeRepository.students.Where(std => std.studentID == id).FirstOrDefault();
            if(stud == null)
            {
                return NotFound("No student found with given Id");
            }
            stud.name = Model.name;
            stud.age = Model.age;
            stud.email = Model.email;
            return NoContent();
        }

        [HttpPatch]
        [Route("UpdatePartial")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
            public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<studentDTO> Model)
        {
            if (Model == null || id == 0)
            {
                return BadRequest();
            }
            var stud = collegeRepository.students.Where(std => std.studentID == id).FirstOrDefault();
            if (stud == null)
            {
                return NotFound("No student found with given Id");
            }
            var studDto = new studentDTO
            {
                studentID = stud.studentID,
                name = stud.name,
                age = stud.age,
                email = stud.email
               
            };
            Model.ApplyTo(studDto, ModelState);
            if (!TryValidateModel(studDto))
            {
                return BadRequest(ModelState);
            }
            stud.name = studDto.name;
            stud.age = studDto.age;
            stud.email = studDto.email;
            
            return NoContent();
        }

    }
}
