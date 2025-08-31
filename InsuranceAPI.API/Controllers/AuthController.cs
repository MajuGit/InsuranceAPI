using InsuranceAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Simple demo check (replace with DB check)
            if (request.Username == "admin" && request.Password == "admin123")
            {
                var token = _authService.GenerateToken(request.Username, "Admin");
                return Ok(new { Token = token });
            }
            else if (request.Username == "user" && request.Password == "user123")
            {
                var token = _authService.GenerateToken(request.Username, "User");
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
