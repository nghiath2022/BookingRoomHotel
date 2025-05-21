using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            var createdCustomer = await _unitOfWork.Customers.CreateAsync(customer);
            await _unitOfWork.CompleteAsync(); // Save changes
            return createdCustomer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            var updatedCustomer = await _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.CompleteAsync(); // Save changes
            return updatedCustomer;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _unitOfWork.Customers.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.CompleteAsync(); // Save changes
            }
            return deleted;
        }
    }
}
