using BookingRoom.Interfaces;
namespace BookingRoom.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return _jwtService.GenerateToken(user);
        }
    }
}
