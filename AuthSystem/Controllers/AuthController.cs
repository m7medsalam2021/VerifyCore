using Core.DTOs;
using Core.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST /api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Username) ||
                    string.IsNullOrEmpty(request.Password) ||
                    string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new { Message = "All fields are required" });
                }

                if (request.Password.Length < 6)
                {
                    return BadRequest(new { Message = "Password must be at least 6 characters long" });
                }

                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email
                };

                _authService.Register(user, request.Password);

                return Ok(new { Message = "User registered successfully", UserId = user.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _authService.Login(request.Username, request.Password);
                return Ok(new
                {
                    Message = "Login successful",
                    UserId = user.Id,
                    Username = user.Username,
                    IsAdmin = user.IsAdmin
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
