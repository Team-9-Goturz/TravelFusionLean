using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.Models;

namespace TravelFusionLeanApi.Controllers
{
    /// <summary>
    /// API-controller til håndtering af TravelPackages.
    /// Understøtter oprettelse, hentning, opdatering og sletning.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TravelPackageController : ControllerBase
    {
        private readonly ITravelPackageService _travelPackageService;

        public TravelPackageController(ITravelPackageService travelPackageService)
        {
            _travelPackageService = travelPackageService;
        }

        /// <summary>
        /// Henter alle rejsepakker.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelPackage>>> GetAllAsync()
        {
            var result = await _travelPackageService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Henter en rejsepakke baseret på ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelPackage>> GetByIdAsync(int id)
        {
            var package = await _travelPackageService.GetByIdAsync(id);
            if (package == null) return NotFound();
            return Ok(package);
        }

        /// <summary>
        /// Opretter en ny rejsepakke.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TravelPackage>> AddAsync(TravelPackage travelPackage)
        {
            if (travelPackage == null)
                return BadRequest("Input mangler.");

            var created = await _travelPackageService.AddAsync(travelPackage);

            // Simpel version til test:
            return Ok(created);

            // Alternativ:
            // return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }


        /// <summary>
        /// Opdaterer en eksisterende rejsepakke.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TravelPackage>> UpdateAsync(int id, TravelPackage travelPackage)
        {
            if (id != travelPackage.Id) return BadRequest("ID mismatch");
            var updated = await _travelPackageService.UpdateAsync(travelPackage);
            return Ok(updated);
        }

        /// <summary>
        /// Sletter en rejsepakke baseret på ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _travelPackageService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
