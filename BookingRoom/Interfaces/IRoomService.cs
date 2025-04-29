using BookingRoom.Models;

namespace BookingRoom.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(Guid id);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(Guid id);
    }
}
