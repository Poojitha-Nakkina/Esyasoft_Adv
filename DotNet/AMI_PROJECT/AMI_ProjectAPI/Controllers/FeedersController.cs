using AMI_ProjectAPI.Data;
using AMI_ProjectAPI.Data.Repository;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedersController : ControllerBase
    {

        private readonly IGenericRepository<Feeder> _feederRepo;
        public FeedersController(IGenericRepository<Feeder> feederRepo)
        {
            _feederRepo = feederRepo;
        }
        [HttpGet("AllFeeders")]
        public async Task<IActionResult> GetAllFeeders()
        {
            var values = await _feederRepo.GetAllAsync();
            return Ok(values);
        }
        [HttpGet("feeder/{id}")]
        public async Task<IActionResult> GetFeederById(int id)
        {
            var val = await _feederRepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }
        [HttpPost("addFeeder")]
        public async Task<IActionResult> CreateFeeder([FromBody] FeederDTO feeder)
        {
            var newFeeder = new Feeder
            {
                FeederId = feeder.FeederId,
                SubstationId = feeder.SubstationId,
                FeederName = feeder.FeederName
            };
            await _feederRepo.AddAsync(newFeeder);
            return CreatedAtAction(nameof(GetFeederById), new { id = feeder.FeederId }, feeder);
        }
        [HttpPut("UpdateFeeder/{id}")]
        public async Task<IActionResult> UpdateFeeder(int id, [FromBody] FeederDTO feeder)
        {
            if (id != feeder.FeederId)
            {
                return BadRequest();
            }
            var updatedFeeder = new Feeder
            {
                FeederId = id,
                SubstationId = feeder.SubstationId,
                FeederName = feeder.FeederName
            };
            await _feederRepo.UpdateAsync(updatedFeeder);
            return NoContent();
        }
        [HttpDelete("DeleteFeeder/{id}")]
        public async Task<IActionResult> DeleteFeeder(int id)
        {
            var existingFeeder = await _feederRepo.GetByIdAsync(id);
            if (existingFeeder == null)
            {
                return NotFound();
            }
            await _feederRepo.DeleteAsync(id);
            return NoContent();
        }

    }
}
