using AMI_ProjectAPI.Data;
using AMI_ProjectAPI.Data.Repository;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IGenericRepository<User> _userrepo;
        private readonly IGenericRepository<Consumer> _consumerRepo;
        public UserController(IGenericRepository<User> userrepo, IGenericRepository<Consumer> consumerRepo)
        {
            _userrepo = userrepo;
            _consumerRepo = consumerRepo;
        }

        [HttpGet("AllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            var values = await _userrepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("Users/{id}")]

        public async Task<IActionResult> GetUsersById(int id)
        {
            var val = await _userrepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO users)
        {
            var newUser = new User
            {
                UserId = users.UserId,
                UserName = users.UserName,
                Email = users.Email,
                Role = users.Role,
                Phone = users.Phone,
                Active = users.Active,
                DisplayName = users.DisplayName
            };
            await _userrepo.AddAsync(newUser);
            return CreatedAtAction(nameof(GetUsersById), new { id = users.UserId }, users);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.UserId)
                return BadRequest("User ID mismatch.");

            var existingUser = await _userrepo.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound("User not found.");

            // ✅ Only update editable fields
            existingUser.UserName = userDto.UserName;
            existingUser.Email = userDto.Email;
            existingUser.Role = userDto.Role;
            existingUser.Phone = userDto.Phone;
            existingUser.Active = userDto.Active;
            existingUser.DisplayName = userDto.DisplayName;

            if (existingUser.ConsumerId.HasValue)
            {
                var consumers = await _consumerRepo.GetAllAsync();
                var consumer = consumers.FirstOrDefault(c => c.ConsumerId == existingUser.ConsumerId.Value);
                if (consumer != null)
                {
                    consumer.Name = userDto.UserName;
                    consumer.Email = userDto.Email;
                    consumer.Phone = userDto.Phone;
                    consumer.UpdatedAt = DateTime.UtcNow;
                    consumer.UpdatedBy = userDto.UserName;
                    await _consumerRepo.UpdateAsync(consumer);
                }
            }

            await _userrepo.UpdateAsync(existingUser);
            return Ok(existingUser);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userrepo.DeleteAsync(id);
            return NoContent();
        }

    }
}