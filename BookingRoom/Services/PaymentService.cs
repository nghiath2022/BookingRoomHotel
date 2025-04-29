using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid id)
        {
            return await _paymentRepository.GetPaymentByIdAsync(id);
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            return await _paymentRepository.CreatePaymentAsync(payment);
        }

        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            return await _paymentRepository.UpdatePaymentAsync(payment);
        }

        public async Task<bool> DeletePaymentAsync(Guid id)
        {
            return await _paymentRepository.DeletePaymentAsync(id);
        }
    }
}
