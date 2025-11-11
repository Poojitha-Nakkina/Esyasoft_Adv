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
    public class TariffSlabController : ControllerBase
    {
        private readonly IGenericRepository<TariffSlab> _tariffSlabRepo;

        public TariffSlabController(IGenericRepository<TariffSlab> tariffSlabRepo)
        {
            _tariffSlabRepo = tariffSlabRepo;
        }

        [HttpGet("AllTariffSlabs")]

        public async Task<IActionResult> GetAllSlabs() {
            var values = await _tariffSlabRepo.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("slabId/{id}")]

        public async Task<IActionResult> GetSlabsById(int id)
        {
            var val = await _tariffSlabRepo.GetByIdAsync(id);
            if(val == null)
            {
                return NotFound();
            }
            return Ok(val);
        }

        [HttpPost("createSlab")]
        public async Task<IActionResult> CreateTariff([FromBody] TariffSlabDTO tariffSlab)
        {
            var newTariffSlab = new TariffSlab
            {
               SlabId = tariffSlab.SlabId,
               TariffId = tariffSlab.TariffId,
               FromKwh = tariffSlab.FromKwh,
               ToKwh = tariffSlab.ToKwh,
               RatePerKwh = tariffSlab.RatePerKwh
            };
            await _tariffSlabRepo.AddAsync(newTariffSlab);
            return CreatedAtAction(nameof(GetSlabsById), new { id = tariffSlab.SlabId }, tariffSlab);
        }

        [HttpPut("UpdateTariffSlab/{id}")]
        public async Task<IActionResult> UpdateTariffSlab(int id, [FromBody] TariffSlabDTO tariffSlab)
        {
            if (id != tariffSlab.SlabId)
            {
                return BadRequest();
            }
            var updatedTariffSlab = new TariffSlab
            {
              

                SlabId = id,
                TariffId = tariffSlab.TariffId,
                FromKwh = tariffSlab.FromKwh,
                ToKwh = tariffSlab.ToKwh,
                RatePerKwh = tariffSlab.RatePerKwh
            };
            await _tariffSlabRepo.UpdateAsync(updatedTariffSlab);
            return NoContent();
        }

        [HttpDelete("DeleteTariffSlab/{id}")]
        public async Task<IActionResult> DeleteTariffSlab(int id)
        {
            await _tariffSlabRepo.DeleteAsync(id);
            return NoContent();
        }

    }
}
