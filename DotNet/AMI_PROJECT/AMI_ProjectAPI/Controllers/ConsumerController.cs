


using AMI_ProjectAPI.Data;
using AMI_ProjectAPI.Data.Repository;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AMI_ProjectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IGenericRepository<Consumer> _consumerRepo;
        private readonly IGenericRepository<User> _userRepo;

        public ConsumerController(IGenericRepository<Consumer> consumerRepo, IGenericRepository<User> userRepo)
        {
            _consumerRepo = consumerRepo;
            _userRepo = userRepo;
        }


        [HttpGet("AllConsumers")]

        public async Task<IActionResult> GetAllConsumers()
        {
            var values = await _consumerRepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("ConsumerById/{id}")]

        public async Task<IActionResult> GetConsumerById(int id)
        {
            var val = await _consumerRepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }
        [HttpPost("addConsumers")]
        public async Task<IActionResult> AddConsumers([FromBody] ConsumerDTO consumers)
        {
            var newConsumer = new Consumer
            {
                ConsumerId = consumers.ConsumerId,
                Name = consumers.Name,
                Address = consumers.Address,
                Phone = consumers.Phone,
                Email = consumers.Email,
                TariffId = consumers.TariffId,
                Status = consumers.Status,
                CreatedAt = consumers.CreatedAt,
                CreatedBy = consumers.CreatedBy,
                UpdatedAt = consumers.UpdatedAt,
                UpdatedBy = consumers.UpdatedBy
            };

            await _consumerRepo.AddAsync(newConsumer);

            // ✅ Optional: Update linked User info if it exists
            var users = await _userRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == consumers.Email);

            if (user != null)
            {
                user.ConsumerId = newConsumer.ConsumerId;
                user.UserName = consumers.Name;
                user.Phone = consumers.Phone;
                await _userRepo.UpdateAsync(user);
            }

            return CreatedAtAction(nameof(GetConsumerById), new { id = newConsumer.ConsumerId }, consumers);
        }

        [HttpPut("UpdateConsumers/{id}")]
        public async Task<IActionResult> UpdateConsumers(int id, [FromBody] ConsumerDTO consumers)
        {
            if (id != consumers.ConsumerId) return BadRequest();

            var updatedConsumer = new Consumer
            {
                ConsumerId = id,
                Name = consumers.Name,
                Address = consumers.Address,
                Phone = consumers.Phone,
                Email = consumers.Email,
                TariffId = consumers.TariffId,
                Status = consumers.Status,
                CreatedAt = consumers.CreatedAt,
                CreatedBy = consumers.CreatedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = consumers.UpdatedBy
            };

            await _consumerRepo.UpdateAsync(updatedConsumer);

            // ✅ Sync corresponding User data
            var users = await _userRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.ConsumerId == id);

            if (user != null)
            {
                user.UserName = consumers.Name;
                user.Email = consumers.Email;
                user.Phone = consumers.Phone;
                await _userRepo.UpdateAsync(user);
            }

            return NoContent();
        }


        [HttpPut("UpdateConsumer/{id}")]
        public async Task<IActionResult> UpdateConsumers(int id, [FromBody] ConsumerProfileUpdateDTO dto)
        {
            // Fetch the existing consumer
            var consumer = await _consumerRepo.GetByIdAsync(id);
            if (consumer == null)
                return NotFound(new { message = "Consumer not found" });

            // Update only editable fields
            consumer.Name = dto.Name;
            consumer.Email = dto.Email;
            consumer.Phone = dto.Phone;
            consumer.Address = dto.Address;
            //consumer.UpdatedAt = DateTime.UtcNow;
            //consumer.UpdatedBy = "User"; // optionally set from JWT claims

            await _consumerRepo.UpdateAsync(consumer);

            var users = await _userRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.ConsumerId == id);

            if (user != null)
            {
                user.UserName = consumer.Name;
                user.Email = consumer.Email;
                user.Phone = consumer.Phone;
                await _userRepo.UpdateAsync(user);
            }

            // Return updated data to frontend
            return Ok(new
            {
                consumer.ConsumerId,
                consumer.Name,
                consumer.Email,
                consumer.Phone,
                consumer.Address,
                //consumer.UpdatedAt
            });
        }

        [HttpDelete("DeleteConsumers/{id}")]
        public async Task<IActionResult> DeleteConsumer(int id)
        {
            await _consumerRepo.DeleteAsync(id);
            return NoContent();
        }


    }
}