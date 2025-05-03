using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoomType>> GetAllAsync()
        {
            return await _unitOfWork.RoomTypes.GetAllAsync();
        }

        public async Task<RoomType?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.RoomTypes.GetByIdAsync(id);
        }

        public async Task<RoomType> CreateAsync(RoomType roomType)
        {
            await _unitOfWork.RoomTypes.AddAsync(roomType);
            await _unitOfWork.CompleteAsync();
            return roomType;
        }

        public async Task<RoomType> UpdateAsync(RoomType roomType)
        {
            await _unitOfWork.RoomTypes.UpdateAsync(roomType);
            await _unitOfWork.CompleteAsync();
            return roomType;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.RoomTypes.DeleteAsync(id);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
