using BookingRoom.Models;

namespace BookingRoom.Interfaces
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetAllAsync();
        Task<RoomType?> GetByIdAsync(Guid id);
        Task<RoomType> CreateAsync(RoomType roomType);
        Task<RoomType> UpdateAsync(RoomType roomType);
        Task<bool> DeleteAsync(Guid id);
    }
}
