using BookingRoom.Interfaces;
using BookingRoom.Models;

namespace BookingRoom.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Customer>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Customer?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task<Customer> CreateAsync(Customer customer) => _repository.CreateAsync(customer);
        public Task<Customer> UpdateAsync(Customer customer) => _repository.UpdateAsync(customer);
        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
