using BookingRoom.Models;

namespace BookingRoom.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
    }
}
