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
    public class SubstationController : ControllerBase
    {
        private readonly IGenericRepository<Substation> _substatrepo;
        public SubstationController(IGenericRepository<Substation> substatrepo)
        {
            _substatrepo = substatrepo;
        }

        [HttpGet("AllSubstations")]

        public async Task<IActionResult> GetAllSubstations()
        {
            var values = await _substatrepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("substation/{id}")]

        public async Task<IActionResult> GetSubstationById(int id)
        {
            var val = await _substatrepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("addSubstation")]
        public async Task<IActionResult> CreateSubstation([FromBody] SubstationDTO substation)
        {
            var newSubstation = new Substation
            {
               SubstationId = substation.SubstationId,
               ZoneId = substation.ZoneId,
               SubstationName = substation.SubstationName
            };
            await _substatrepo.AddAsync(newSubstation);
            return CreatedAtAction(nameof(GetSubstationById), new { id = substation.SubstationId }, substation);
        }

        [HttpPut("UpdateSubstation/{id}")]
        public async Task<IActionResult> UpdateSubstation(int id, [FromBody] SubstationDTO substation)
        {
            if (id != substation.SubstationId)
            {
                return BadRequest();
            }
            var updatedSubstation = new Substation
            {
                SubstationId = id,
                ZoneId = substation.ZoneId,
                SubstationName= substation.SubstationName

                
            };
            await _substatrepo.UpdateAsync(updatedSubstation);
            return NoContent();
        }

        [HttpDelete("DeleteSubstation/{id}")]
        public async Task<IActionResult> DeleteSubstation(int id)
        {
            await _substatrepo.DeleteAsync(id);
            return NoContent();
        }

    }
}
