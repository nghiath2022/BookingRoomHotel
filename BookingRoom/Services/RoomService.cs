using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllRoomsAsync();
        }

        public async Task<Room> GetRoomByIdAsync(Guid id)
        {
            return await _roomRepository.GetRoomByIdAsync(id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            return await _roomRepository.CreateRoomAsync(room);
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            return await _roomRepository.UpdateRoomAsync(room);
        }

        public async Task<bool> DeleteRoomAsync(Guid id)
        {
            return await _roomRepository.DeleteRoomAsync(id);
        }
    }
}
