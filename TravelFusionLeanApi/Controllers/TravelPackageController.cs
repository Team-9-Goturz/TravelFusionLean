using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.Models;

namespace TravelFusionLeanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelPackageController : ControllerBase
    {
        private readonly ITravelPackageService travelPackageService;

        public TravelPackageController(ITravelPackageService travelPackageService)
        {
             this.travelPackageService = travelPackageService;
        }

        [HttpGet("All-Packages")]
        public async Task<ActionResult<IEnumerable<TravelPackage>>> GetAllPackagesAsync()
        {
            var packages = await travelPackageService.GetAllAsync();
            return Ok(packages);

        }

        [HttpGet("Packages-By-Id/{id}")]
        public async Task<ActionResult<TravelPackage>> GetPackagesByIdAsync(Guid id)
        {
            var packages = await travelPackageService.GetByIdAsync(id);
            return Ok(packages);

        }

        [HttpPost("Add-Package")]
        public async Task<ActionResult<TravelPackage>> AddPackageAsync(TravelPackage travelPackage)
        {

            await travelPackageService.AddAsync(travelPackage);
            return Ok();
        }


        [HttpPut("Update-Package")]
        public IActionResult UpdatePackage(TravelPackage travelPackage)
        {
            travelPackageService.UpdateAsync(travelPackage);
            return Ok();
        }


        [HttpDelete("Delete-Package-By-Id/{id}")]
        public async Task<IActionResult> DeletePackageAsync(Guid id)
        {
            var package = await travelPackageService.GetByIdAsync(id);
            if (package == null) return NotFound();

            travelPackageService.DeleteAsync(package);
            return Ok();
        }
    }
}
