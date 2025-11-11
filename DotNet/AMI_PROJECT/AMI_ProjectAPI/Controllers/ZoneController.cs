using AMI_ProjectAPI.Data;
using AMI_ProjectAPI.Data.Repository;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IGenericRepository<Zone> _zonerepo;
        public ZoneController(IGenericRepository<Zone> zonerepo) { 
            _zonerepo = zonerepo;
        }

        [HttpGet("AllZones")]

        public async Task<IActionResult> GetAllZones()
        {
            var values = await _zonerepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("zone/{id}")]

        public async Task<IActionResult> GetZonesById(int id)
        {
            var val = await _zonerepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("addZone")]
        public async Task<IActionResult> CreateZone([FromBody] ZoneDTO zone)
        {
            var newZone = new Zone
            {
                ZoneId = zone.ZoneId,
                ZoneName = zone.ZoneName
            };
            await _zonerepo.AddAsync(newZone);
            return CreatedAtAction(nameof(GetZonesById), new { id = zone.ZoneId }, zone);
        }

        [HttpPut("UpdateZone/{id}")]
        public async Task<IActionResult> UpdateZone(int id, [FromBody] ZoneDTO zone)
        {
            if (id != zone.ZoneId)
            {
                return BadRequest();
            }
            var updatedzone = new Zone
            {


               ZoneId = id,
               ZoneName = zone.ZoneName,
            };
            await _zonerepo.UpdateAsync(updatedzone);
            return NoContent();
        }

        [HttpDelete("DeleteZone/{id}")]
        public async Task<IActionResult> DeleteZone(int id)
        {
            await _zonerepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
