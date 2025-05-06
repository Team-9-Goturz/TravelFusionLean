//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using ServiceContracts;
//using TravelFusionLeanApi.Configuration;
//using TravelFusionLean.Models;
//using Shared.Dtos;
//using Shared.DTOs;

//namespace TravelFusionLeanApi.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly IUserService _userService;
//        private readonly IConfiguration _config;

//        public AuthController(IUserService userService, IConfiguration config)
//        {
//            _userService = userService;
//            _config = config;
//        }

//        [HttpPost("login")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
//        {
//            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
//                return BadRequest("Username and password are required.");

//            var user = await _userService.AuthenticateByUsernameAsync(dto.Username, dto.Password); 

//            if (user == null)
//                return Unauthorized("Invalid username or password.");

//            var token = JwtTokenHelper.GenerateToken(user, _config);

//            return Ok(new
//            {
//                token,
//                username = user.Username,
//                role = user.UserRole?.Name ?? "User"
//            });
//        }

//    }
//}
