using AMI_ProjectAPI.Data;
using AMI_ProjectAPI.Data.Repository;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly IGenericRepository<MeterReading> _meterReadingRepo;
        public MeterReadingController(IGenericRepository<MeterReading> meterReadingRepo)
        {
            _meterReadingRepo = meterReadingRepo;
        }

        [HttpGet("AllMeterReadings")]

        public async Task<IActionResult> GetAllMeterReadings()
        {
            var values = await _meterReadingRepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("MeterReadings/{id}")]

        public async Task<IActionResult> GetMeterReadingById(int id)
        {
            var val = await _meterReadingRepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("addMeterReadings")]
        public async Task<IActionResult> AddMeterReadings([FromBody] MeterReadingDTO meterReadings)
        {
            var NewmeterReading = new MeterReading
            {
               ReadingId = meterReadings.ReadingId,
               MeterId = meterReadings.MeterId,
               ReadingValue = meterReadings.ReadingValue,
               ReadingDate = meterReadings.ReadingDate,
               CreatedAt = meterReadings.CreatedAt

            };
            await _meterReadingRepo.AddAsync(NewmeterReading);
            return CreatedAtAction(nameof(GetMeterReadingById), new { id = meterReadings.ReadingId }, meterReadings);
        }

        [HttpPut("UpdateMeterReadings/{id}")]
        public async Task<IActionResult> UpdateMeterReadings(int id, [FromBody] MeterReadingDTO meterReadings)
        {
            if (id != meterReadings.ReadingId)
            {
                return BadRequest();
            }
            var updatedMeterreading = new MeterReading
            {
               ReadingId = id,
               MeterId= meterReadings.MeterId,
               ReadingValue= meterReadings.ReadingValue,
               ReadingDate= meterReadings.ReadingDate,
               CreatedAt = meterReadings.CreatedAt

            };
            await _meterReadingRepo.UpdateAsync(updatedMeterreading);
            return NoContent();
        }

        [HttpDelete("DeleteMeterReading/{id}")]
        public async Task<IActionResult> DeleteMeter(int id)
        {
            await _meterReadingRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
