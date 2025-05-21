using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _unitOfWork.Payments.GetAllPaymentsAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(Guid id)
        {
            return await _unitOfWork.Payments.GetPaymentByIdAsync(id);
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            var createdPayment = await _unitOfWork.Payments.CreatePaymentAsync(payment);
            await _unitOfWork.CompleteAsync(); // Save changes
            return createdPayment;
        }

        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            var updatedPayment = await _unitOfWork.Payments.UpdatePaymentAsync(payment);
            await _unitOfWork.CompleteAsync(); // Save changes
            return updatedPayment;
        }

        public async Task<bool> DeletePaymentAsync(Guid id)
        {
            var deleted = await _unitOfWork.Payments.DeletePaymentAsync(id);
            if (deleted)
            {
                await _unitOfWork.CompleteAsync(); // Save changes
            }
            return deleted;
        }
    }
}
