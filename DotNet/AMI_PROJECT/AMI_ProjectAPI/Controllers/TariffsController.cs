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
    public class TariffsController : ControllerBase
    {
        private readonly IGenericRepository<Tariff> _tariffRepository;
        //private readonly AmiContext _context;
        public TariffsController(IGenericRepository<Tariff> tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }

        [HttpGet("GetAllTariffs")]
        public async Task<IActionResult> GetAllTariffs()
        {
            var tariffs = await _tariffRepository.GetAllAsync();
            return Ok(tariffs);
        }
        [HttpGet("GetTariffById/{id}")]
        public async Task<IActionResult> GetTariffById(int id)
        {
            var tariff = await _tariffRepository.GetByIdAsync(id);
            if (tariff == null)
            {
                return NotFound();
            }
            return Ok(tariff);
        }
        [HttpPost("CreateTariff")]
        public async Task<IActionResult> CreateTariff([FromBody] TariffDTO tariff)
        {
            var newTariff = new Tariff
            {
                Name = tariff.Name,
                EffectiveFrom = tariff.EffectiveFrom,
                EffectiveTo = tariff.EffectiveTo,
                BaseRate = tariff.BaseRate,
                TaxRate = tariff.TaxRate
            };
            await _tariffRepository.AddAsync(newTariff);
            return CreatedAtAction(nameof(GetTariffById), new { id = tariff.TariffId }, tariff);
        }
        [HttpPut("UpdateTariff/{id}")]
        public async Task<IActionResult> UpdateTariff(int id, [FromBody] TariffDTO tariff)
        {
            if (id != tariff.TariffId)
            {
                return BadRequest();
            }
            var updatedTariff = new Tariff
            {
                TariffId = id,
                Name = tariff.Name,
                EffectiveFrom = tariff.EffectiveFrom,
                EffectiveTo = tariff.EffectiveTo,
                BaseRate = tariff.BaseRate,
                TaxRate = tariff.TaxRate
            };
            await _tariffRepository.UpdateAsync(updatedTariff);
            return NoContent();
        }
        [HttpDelete("DeleteTariff/{id}")]
        public async Task<IActionResult> DeleteTariff(int id)
        {
            await _tariffRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}
