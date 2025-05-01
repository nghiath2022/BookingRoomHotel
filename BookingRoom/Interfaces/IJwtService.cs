using BookingRoom.Models;

namespace BookingRoom.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }

}
