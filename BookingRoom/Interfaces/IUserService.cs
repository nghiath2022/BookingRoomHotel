using BookingRoom.DTOs;
using BookingRoom.Models;

namespace BookingRoom.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> RegisterUserAsync(RegisterRequest request);
    }
}
