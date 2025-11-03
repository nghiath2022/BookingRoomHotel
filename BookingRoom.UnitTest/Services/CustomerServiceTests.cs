using BookingRoom.Interfaces;
using BookingRoom.Models;
using BookingRoom.Services;
using FluentAssertions;
using Moq;

namespace BookingRoom.UnitTest.Services
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICustomerRepository> _mockCustomerRepo;

        public CustomerServiceTests()
        {
            _mockCustomerRepo = new Mock<ICustomerRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Gán repository giả vào unit of work
            _mockUnitOfWork.Setup(u => u.Customers).Returns(_mockCustomerRepo.Object);

            _customerService = new CustomerService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), FullName = "Alice Nguyen", Email = "alice@mail.com" },
                new Customer { Id = Guid.NewGuid(), FullName = "Bob Tran", Email = "bob@mail.com" }
            };

            _mockCustomerRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer()
        {
            // Arrange
            var id = Guid.NewGuid();
            var customer = new Customer { Id = id, FullName = "Charlie", Email = "charlie@mail.com" };

            _mockCustomerRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result!.FullName.Should().Be("Charlie");
            result.Email.Should().Be("charlie@mail.com");
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedCustomer_AndCallComplete()
        {
            // Arrange
            var newCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = "Daisy Le",
                Email = "daisy@mail.com",
                Phone = "0123456789",
                Address = "123 Street",
                IdentityNumber = "ID123456"
            };

            _mockCustomerRepo.Setup(r => r.CreateAsync(newCustomer)).ReturnsAsync(newCustomer);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _customerService.CreateAsync(newCustomer);

            // Assert
            result.Should().BeEquivalentTo(newCustomer);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedCustomer_AndCallComplete()
        {
            // Arrange
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = "Eve Pham",
                Email = "eve@mail.com"
            };

            _mockCustomerRepo.Setup(r => r.UpdateAsync(customer)).ReturnsAsync(customer);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _customerService.UpdateAsync(customer);

            // Assert
            result.Should().BeEquivalentTo(customer);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenSuccess_ShouldCallCompleteAndReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockCustomerRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _customerService.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenFails_ShouldNotCallCompleteAndReturnFalse()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockCustomerRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _customerService.DeleteAsync(id);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
