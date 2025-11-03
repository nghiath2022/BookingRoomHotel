using BookingRoom.DTOs;
using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllUsersAsync();
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _unitOfWork.Users.GetUserByIdAsync(id);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            var created = await _unitOfWork.Users.CreateUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return created;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var updated = await _unitOfWork.Users.UpdateUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return updated;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var deleted = await _unitOfWork.Users.DeleteUserAsync(id);
            if (deleted)
                await _unitOfWork.CompleteAsync();
            return deleted;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.Users.GetUserByEmailAsync(email);
        }

        public async Task<User> RegisterUserAsync(RegisterRequest request)
        {
            var existingUser = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email already registered.");

            if (request.RoleName.ToLower() == "admin")
            {
                var anyAdmin = await _unitOfWork.Users.CheckAdminExistsAsync();
                if (anyAdmin)
                    throw new UnauthorizedAccessException("Admin already exists.");
            }

            var role = await _unitOfWork.Users.GetRoleByNameAsync(request.RoleName);
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

            var createdUser = await _unitOfWork.Users.CreateUserAsync(user);
            await _unitOfWork.CompleteAsync();

            return createdUser;
        }
    }
}
