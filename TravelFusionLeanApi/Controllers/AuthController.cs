using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Models;

namespace TravelFusionLeanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            // Dummy login-check – her burde der være databaseopslag
            if (loginDto.Username == "admin" && loginDto.Password == "1234")
            {
                return Ok("Login successful");
            }

            return Unauthorized("Invalid credentials");
        }
    }
}