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
    public class DtrController : ControllerBase
    {
        private readonly IGenericRepository<Dtr> _dtrrepo;
        public DtrController(IGenericRepository<Dtr> dtrrepo)
        {
            _dtrrepo = dtrrepo;
        }
        [HttpGet("AllDtrs")]
        public async Task<IActionResult> GetAllDtrs()
        {
            var values = await _dtrrepo.GetAllAsync();
            return Ok(values);
        }
        [HttpGet("dtr/{id}")]
        public async Task<IActionResult> GetDtrById(int id)
        {
            var val = await _dtrrepo.GetByIdAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }
        [HttpPost("addDtr")]
        public async Task<IActionResult> CreateDtr([FromBody] DtrDTO dtr)
        {
            var newDtr = new Dtr
            {
                Dtrid = dtr.Dtrid,
                FeederId = dtr.FeederId,
                Dtrname = dtr.Dtrname
            };
            await _dtrrepo.AddAsync(newDtr);
            return CreatedAtAction(nameof(GetDtrById), new { id = dtr.Dtrid }, dtr);
        }
        [HttpPut("UpdateDtr/{id}")]
        public async Task<IActionResult> UpdateDtr(int id, [FromBody] DtrDTO dtr)
        {
            if (id != dtr.Dtrid)
            {
                return BadRequest();
            }
            var updatedDtr = new Dtr
            {
                Dtrid = id,
                FeederId = dtr.FeederId,
                Dtrname = dtr.Dtrname
            };
            await _dtrrepo.UpdateAsync(updatedDtr);
            return NoContent();
        }
        [HttpDelete("DeleteDtr/{id}")]
        public async Task<IActionResult> DeleteDtr(int id)
        {
            var existingDtr = await _dtrrepo.GetByIdAsync(id);
            if (existingDtr == null)
            {
                return NotFound();
            }
            await _dtrrepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
