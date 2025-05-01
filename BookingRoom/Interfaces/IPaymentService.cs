using BookingRoom.Models;

namespace BookingRoom.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(Guid id);
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(Guid id);
    }
}
