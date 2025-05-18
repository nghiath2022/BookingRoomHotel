using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _unitOfWork.Rooms.GetAllRoomsAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(Guid id)
        {
            return await _unitOfWork.Rooms.GetRoomByIdAsync(id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            var createdRoom = await _unitOfWork.Rooms.CreateRoomAsync(room);
            await _unitOfWork.CompleteAsync(); // Save changes
            return createdRoom;
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            var updatedRoom = await _unitOfWork.Rooms.UpdateRoomAsync(room);
            await _unitOfWork.CompleteAsync(); // Save changes
            return updatedRoom;
        }

        public async Task<bool> DeleteRoomAsync(Guid id)
        {
            var deleted = await _unitOfWork.Rooms.DeleteRoomAsync(id);
            if (deleted)
            {
                await _unitOfWork.CompleteAsync(); // Save changes
            }
            return deleted;
        }
    }
}
