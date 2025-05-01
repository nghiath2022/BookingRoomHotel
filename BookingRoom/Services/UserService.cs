using BookingRoom.DTOs;
using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<User> RegisterUserAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email already registered.");

            if (request.RoleName.ToLower() == "admin")
            {
                var anyAdmin = await _userRepository.CheckAdminExistsAsync();

                if (anyAdmin)
                    throw new UnauthorizedAccessException("Admin already exists.");
            }

            var role = await _userRepository.GetRoleByNameAsync(request.RoleName);
            if (role == null)
                throw new Exception("Role not found.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                CreatedAt = DateTime.UtcNow
            };

            return await _userRepository.CreateUserAsync(user);
        }
    }
}
