
using BookingRoom.DTOs;
using BookingRoom.Interfaces;
using BookingRoom.Models;
using BookingRoom.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingRoom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request.Email, request.Password);

            if (token == null)
                return Unauthorized(new { message = "Invalid credentials." });

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Check email Existed ? 
            var existing = await _userService.GetUserByEmailAsync(request.Email);
            if (existing != null)
                return BadRequest(new { message = "Email already registered." });

            // Encrypted password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Mặc định Role = User
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // Role: User
                CreatedAt = DateTime.UtcNow
            };

            var created = await _userService.CreateUserAsync(user);

            return Ok(new { message = "User registered successfully.", userId = created.Id });
        }
    }
}
