using AutoMapper;
using BookingRoom.Controllers;
using BookingRoom.DTOs;
using BookingRoom.Interfaces;
using BookingRoom.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookingRoom.UnitTest.Controllers
{
    public class BookingControllerTests
    {
        private readonly BookingController _controller;
        private readonly Mock<IBookingService> _mockBookingService;
        private readonly Mock<IMapper> _mockMapper;

        public BookingControllerTests()
        {
            _mockBookingService = new Mock<IBookingService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new BookingController(_mockBookingService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfBookingDto()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new Booking { Id = Guid.NewGuid(), TotalPrice = 1000 },
                new Booking { Id = Guid.NewGuid(), TotalPrice = 1500 }
            };

                    var bookingDtos = new List<BookingDto>
            {
                new BookingDto { Id = bookings[0].Id, TotalPrice = 1000 },
                new BookingDto { Id = bookings[1].Id, TotalPrice = 1500 }
            };

            _mockBookingService.Setup(s => s.GetAllBookingsAsync()).ReturnsAsync(bookings);
            _mockMapper.Setup(m => m.Map<IEnumerable<BookingDto>>(bookings)).Returns(bookingDtos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;

            // Assert
            okResult.Should().NotBeNull();

            var value = okResult!.Value as IEnumerable<BookingDto>;
            value.Should().NotBeNull();
            value.Should().HaveCount(2);
            value.Should().Contain(b => b.TotalPrice == 1000);
            value.Should().Contain(b => b.TotalPrice == 1500);
        }

        [Fact]
        public async Task GetById_ShouldReturnBookingDto_WhenExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var booking = new Booking { Id = id };
            var bookingDto = new BookingDto { Id = id };

            _mockBookingService.Setup(s => s.GetBookingByIdAsync(id)).ReturnsAsync(booking);
            _mockMapper.Setup(m => m.Map<BookingDto>(booking)).Returns(bookingDto);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = result.Result as OkObjectResult;

            // Assert
            okResult.Should().NotBeNull();
            var value = okResult!.Value as BookingDto;
            value.Should().NotBeNull();
            value!.Id.Should().Be(id);
            value.TotalPrice.Should().Be(1200);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNotExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockBookingService.Setup(s => s.GetBookingByIdAsync(id)).ReturnsAsync((Booking?)null);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WithBookingDto()
        {
            // Arrange
            var createDto = new CreateBookingDto
            {
                CustomerId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(2),
                TotalPrice = 500
            };

            var booking = new Booking { Id = Guid.NewGuid(), TotalPrice = createDto.TotalPrice };
            var bookingDto = new BookingDto { Id = booking.Id, TotalPrice = booking.TotalPrice };

            _mockMapper.Setup(m => m.Map<Booking>(createDto)).Returns(booking);
            _mockBookingService.Setup(s => s.CreateBookingAsync(booking)).ReturnsAsync(booking);
            _mockMapper.Setup(m => m.Map<BookingDto>(booking)).Returns(bookingDto);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            var value = createdResult!.Value as BookingDto;
            value.Should().NotBeNull();
            value!.TotalPrice.Should().Be(500);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WithUpdatedDto()
        {
            // Arrange
            var updateDto = new UpdateBookingDto
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(2),
                TotalPrice = 800
            };

            var booking = new Booking { Id = updateDto.Id };
            var bookingDto = new BookingDto { Id = updateDto.Id, TotalPrice = 800 };

            _mockMapper.Setup(m => m.Map<Booking>(updateDto)).Returns(booking);
            _mockBookingService.Setup(s => s.UpdateBookingAsync(booking)).ReturnsAsync(booking);
            _mockMapper.Setup(m => m.Map<BookingDto>(booking)).Returns(bookingDto);

            // Act
            var result = await _controller.Update(updateDto.Id, updateDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var value = okResult!.Value as BookingDto;
            value.Should().NotBeNull();
            value!.TotalPrice.Should().Be(800);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
        {
            var dto = new BookingUpdateDto { Id = Guid.NewGuid() };
            var result = await _controller.Update(Guid.NewGuid(), dto);
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenDeleted()
        {
            var id = Guid.NewGuid();

            _mockBookingService.Setup(s => s.CancelBookingAsync(id)).ReturnsAsync(true);

            var result = await _controller.Cancel(id);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNotDeleted()
        {
            var id = Guid.NewGuid();

            _mockBookingService.Setup(s => s.CancelBookingAsync(id)).ReturnsAsync(false);

            var result = await _controller.Cancel(id);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
