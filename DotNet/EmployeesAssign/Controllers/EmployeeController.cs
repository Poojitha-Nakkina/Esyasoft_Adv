using EmployeesAssign.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeesAssign.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesAssign.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _empRepo;
        public EmployeeController(IEmployeeRepository empRepo) { 
            _empRepo = empRepo;
        }

        [HttpGet("All")]
        public async Task<IActionResult> getAllEmployees()
        {
            var values = await _empRepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> getAllById(int id)
        {
            var values = await _empRepo.GetByIdAsync(id);
            if (values == null) { 
                return NotFound();
            }
            return Ok(values);
        }

        [HttpPost("create")]
        public async Task<IActionResult> createEmployee([FromBody] Employee emp)
        {
            if (emp == null)
            {
                return BadRequest();
            }
            await _empRepo.AddAsync(emp);
            return CreatedAtAction(nameof(getAllById), new { id = emp.EmployeeId }, emp);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> updateEmployee(int id,[FromBody] Employee emp)
        {
            if (emp == null || id!=emp.EmployeeId)
            {
                return BadRequest();
            }
            await _empRepo.UpdateAsync(emp);
            return CreatedAtAction(nameof(getAllById), new { id = emp.EmployeeId }, emp);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> deleteEmployee(int id)
        {
            var emp = await _empRepo.GetByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            _empRepo.DeleteAsync(emp.EmployeeId);
            return NoContent();
        }

    }
}
