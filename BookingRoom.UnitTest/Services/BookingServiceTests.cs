using BookingRoom.Interfaces;
using BookingRoom.Models;
using BookingRoom.Services;
using FluentAssertions;
using Moq;

namespace BookingRoom.UnitTest.Services
{
    public class BookingServiceTests
    {
        private readonly BookingService _bookingService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBookingRepository> _mockBookingRepo;

        public BookingServiceTests()
        {
            _mockBookingRepo = new Mock<IBookingRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(u => u.Bookings).Returns(_mockBookingRepo.Object);

            _bookingService = new BookingService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllBookingsAsync_ShouldReturnAllBookings()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    RoomId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    CheckInDate = DateTime.Today,
                    CheckOutDate = DateTime.Today.AddDays(2),
                    TotalPrice = 300,
                    Status = "Confirmed"
                },
                new Booking
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    RoomId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    CheckInDate = DateTime.Today.AddDays(1),
                    CheckOutDate = DateTime.Today.AddDays(3),
                    TotalPrice = 450,
                    Status = "Pending"
                }
            };

            _mockBookingRepo.Setup(r => r.GetAllBookingsAsync()).ReturnsAsync(bookings);

            // Act
            var result = await _bookingService.GetAllBookingsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().TotalPrice.Should().Be(300);
        }
    }
}
