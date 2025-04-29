using BookingRoom.Data;
using BookingRoom.Interfaces;
using BookingRoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .ThenInclude(r => r.RoomType)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> CancelBookingAsync(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            booking.Status = "Cancelled";
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
