using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _unitOfWork.Bookings.GetAllBookingsAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            return await _unitOfWork.Bookings.GetBookingByIdAsync(id);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            await _unitOfWork.Bookings.CreateBookingAsync(booking);
            await _unitOfWork.CompleteAsync();
            return booking;
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            await _unitOfWork.Bookings.UpdateBookingAsync(booking);
            await _unitOfWork.CompleteAsync();
            return booking;
        }

        public async Task<bool> CancelBookingAsync(Guid id)
        {
            return await _unitOfWork.Bookings.CancelBookingAsync(id);
        }
    }
}
