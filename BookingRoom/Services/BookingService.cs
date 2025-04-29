using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _bookingRepository.GetBookingByIdAsync(id);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            // Business rule: thêm kiểm tra phòng trống, thời gian...
            return await _bookingRepository.CreateBookingAsync(booking);
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            return await _bookingRepository.UpdateBookingAsync(booking);
        }

        public async Task<bool> CancelBookingAsync(Guid id)
        {
            return await _bookingRepository.CancelBookingAsync(id);
        }
    }
}
