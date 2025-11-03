using BookingRoom.Interfaces;
using BookingRoom.Models;
using BookingRoom.Services;
using FluentAssertions;
using Moq;


namespace BookingRoom.UnitTest.Services
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _paymentService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPaymentRepository> _mockPaymentRepo;

        public PaymentServiceTests()
        {
            _mockPaymentRepo = new Mock<IPaymentRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(u => u.Payments).Returns(_mockPaymentRepo.Object);

            _paymentService = new PaymentService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllPaymentsAsync_ShouldReturnListOfPayments()
        {
            // Arrange
            var payments = new List<Payment>
            {
                new Payment { Id = Guid.NewGuid(), BookingId = Guid.NewGuid(), Amount = 100, PaymentMethod = "Credit Card", PaymentDate = DateTime.UtcNow },
                new Payment { Id = Guid.NewGuid(), BookingId = Guid.NewGuid(), Amount = 200, PaymentMethod = "Cash", PaymentDate = DateTime.UtcNow }
            };

            _mockPaymentRepo.Setup(r => r.GetAllPaymentsAsync()).ReturnsAsync(payments);

            // Act
            var result = await _paymentService.GetAllPaymentsAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(p => p.PaymentMethod.Should().NotBeNullOrEmpty());
        }

        [Fact]
        public async Task GetPaymentByIdAsync_ShouldReturnPayment_WhenFound()
        {
            var id = Guid.NewGuid();
            var payment = new Payment
            {
                Id = id,
                BookingId = Guid.NewGuid(),
                Amount = 300,
                PaymentMethod = "Bank Transfer",
                Status = "Completed",
                PaymentDate = DateTime.UtcNow
            };

            _mockPaymentRepo.Setup(r => r.GetPaymentByIdAsync(id)).ReturnsAsync(payment);

            var result = await _paymentService.GetPaymentByIdAsync(id);

            result.Should().NotBeNull();
            result!.Amount.Should().Be(300);
            result.Status.Should().Be("Completed");
        }

        [Fact]
        public async Task CreatePaymentAsync_ShouldReturnCreatedPayment_AndCallComplete()
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Amount = 150,
                PaymentMethod = "Momo",
                Status = "Pending",
                PaymentDate = DateTime.UtcNow
            };

            _mockPaymentRepo.Setup(r => r.CreatePaymentAsync(payment)).ReturnsAsync(payment);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _paymentService.CreatePaymentAsync(payment);

            result.Should().BeEquivalentTo(payment);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdatePaymentAsync_ShouldReturnUpdatedPayment_AndCallComplete()
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Amount = 180,
                PaymentMethod = "ZaloPay",
                Status = "Processing",
                PaymentDate = DateTime.UtcNow
            };

            _mockPaymentRepo.Setup(r => r.UpdatePaymentAsync(payment)).ReturnsAsync(payment);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _paymentService.UpdatePaymentAsync(payment);

            result.Should().BeEquivalentTo(payment);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletePaymentAsync_ShouldReturnTrue_AndCallComplete_WhenDeleted()
        {
            var id = Guid.NewGuid();

            _mockPaymentRepo.Setup(r => r.DeletePaymentAsync(id)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _paymentService.DeletePaymentAsync(id);

            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletePaymentAsync_ShouldReturnFalse_AndNotCallComplete_WhenNotDeleted()
        {
            var id = Guid.NewGuid();

            _mockPaymentRepo.Setup(r => r.DeletePaymentAsync(id)).ReturnsAsync(false);

            var result = await _paymentService.DeletePaymentAsync(id);

            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
