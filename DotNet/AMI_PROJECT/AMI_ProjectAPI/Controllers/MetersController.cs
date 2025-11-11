using AMI_ProjectAPI.Data;
using AMI_ProjectAPI.Data.Repository;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using Meter = AMI_ProjectAPI.Models.Meter;

namespace AMI_ProjectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MetersController : ControllerBase
    {
        private readonly IGenericRepository<Meter> _meterRepo;
        public MetersController(IGenericRepository<Meter> meterRepo)
        {
            _meterRepo = meterRepo;
        }

        [HttpGet("AllMeters")]

        public async Task<IActionResult> GetAllMeters()
        {
            var values = await _meterRepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("ByConsumer/{consumerId}")]
        public async Task<IActionResult> GetMetersByConsumer(int consumerId)
        {
            try
            {
                // Fetch all meters first (you can also query directly from DbContext if needed)
                var allMeters = await _meterRepo.GetAllAsync();

                // Filter by ConsumerId
                var consumerMeters = allMeters.Where(m => m.ConsumerId == consumerId).ToList();

                if (consumerMeters == null || consumerMeters.Count == 0)
                    return NotFound(new { message = "No meters found for this consumer." });

                return Ok(consumerMeters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching meters.", error = ex.Message });
            }
        }

        [HttpGet("Meter/{id}")]

        public async Task<IActionResult> GetMeterById(int id)
        {
            var val = await _meterRepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("addMeters")]
        public async Task<IActionResult> AddMeters([FromBody] MeterDTO meters)
        {
            var newMeter = new Meter
            {
                MeterId = meters.MeterId,
                MeterSerialNo = meters.MeterSerialNo,
                ConsumerId = meters.ConsumerId,
                Dtrid = meters.Dtrid,
                Ipaddress = meters.Ipaddress,
                Iccid = meters.Iccid,
                Imsi = meters.Imsi,
                Manufacturer = meters.Manufacturer,
                Firmware = meters.Firmware,
                Category = meters.Category,
                InstallDate = meters.InstallDate,
                Status = meters.Status

            };
            await _meterRepo.AddAsync(newMeter);
            return CreatedAtAction(nameof(GetMeterById), new { id = meters.MeterId }, meters);
        }

        [HttpPut("UpdateMeters/{id}")]
        public async Task<IActionResult> UpdateMeters(int id, [FromBody] MeterDTO meters)
        {
            if (id != meters.MeterId)
            {
                return BadRequest();
            }
            var updatedMeter = new Meter
            {
                MeterId = id,
                MeterSerialNo = meters.MeterSerialNo,
                ConsumerId= meters.ConsumerId,
                Dtrid= meters.Dtrid,
                Ipaddress = meters.Ipaddress,
                Iccid = meters.Iccid,
                Imsi = meters.Imsi,
                Manufacturer = meters.Manufacturer,
                Firmware = meters.Firmware,
                Category = meters.Category,
                InstallDate = meters.InstallDate,
                Status = meters.Status


            };
            await _meterRepo.UpdateAsync(updatedMeter);
            return NoContent();
        }

        [HttpDelete("DeleteMeter/{id}")]
        public async Task<IActionResult> DeleteMeter(int id)
        {
            await _meterRepo.DeleteAsync(id);
            return NoContent();
        }

    }
}
