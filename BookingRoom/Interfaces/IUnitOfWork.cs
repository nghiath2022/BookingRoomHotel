namespace BookingRoom.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoomRepository Rooms { get; }
        IRoomTypeRepository RoomTypes { get; }
        IBookingRepository Bookings { get; }
        IPaymentRepository Payments { get; }
        ICustomerRepository Customers { get; }

        Task<int> CompleteAsync(); // = SaveChangesAsync()
    }
}
