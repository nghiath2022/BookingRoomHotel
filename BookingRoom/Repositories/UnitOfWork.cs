using BookingRoom.Data;
using BookingRoom.Interfaces;

namespace BookingRoom.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository Users { get; }
        public IRoomRepository Rooms { get; }
        public IRoomTypeRepository RoomTypes { get; }
        public IBookingRepository Bookings { get; }
        public IPaymentRepository Payments { get; }
        public ICustomerRepository Customers { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IUserRepository users,
            IRoomRepository rooms,
            IRoomTypeRepository roomTypes,
            IBookingRepository bookings,
            IPaymentRepository payments,
            ICustomerRepository customers)
        {
            _context = context;
            Users = users;
            Rooms = rooms;
            RoomTypes = roomTypes;
            Bookings = bookings;
            Payments = payments;
            Customers = customers;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
