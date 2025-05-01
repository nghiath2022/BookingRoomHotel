using BookingRoom.Models;

namespace BookingRoom.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllAsync();
        Task<RoomType?> GetByIdAsync(Guid id);
        Task AddAsync(RoomType roomType);
        Task UpdateAsync(RoomType roomType);
        Task DeleteAsync(Guid id);
    }
}
