using BookingRoom.Interfaces;
using BookingRoom.Models;
using BookingRoom.Services;
using FluentAssertions;
using Moq;

namespace BookingRoom.UnitTest.Services
{
    public class RoomTypeServiceTests
    {
        private readonly RoomTypeService _roomTypeService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRoomTypeRepository> _mockRoomTypeRepo;

        public RoomTypeServiceTests()
        {
            _mockRoomTypeRepo = new Mock<IRoomTypeRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(u => u.RoomTypes).Returns(_mockRoomTypeRepo.Object);

            _roomTypeService = new RoomTypeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfRoomTypes()
        {
            var roomTypes = new List<RoomType>
        {
            new RoomType { Id = Guid.NewGuid(), TypeName = "Deluxe", Description = "Sea View" },
            new RoomType { Id = Guid.NewGuid(), TypeName = "Standard", Description = "City View" }
        };

            _mockRoomTypeRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(roomTypes);

            var result = await _roomTypeService.GetAllAsync();

            result.Should().HaveCount(2);
            result.Should().Contain(rt => rt.TypeName == "Deluxe");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRoomType_WhenFound()
        {
            var id = Guid.NewGuid();
            var roomType = new RoomType { Id = id, TypeName = "Suite", Description = "Luxury Room" };

            _mockRoomTypeRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(roomType);

            var result = await _roomTypeService.GetByIdAsync(id);

            result.Should().NotBeNull();
            result!.TypeName.Should().Be("Suite");
        }

        [Fact]
        public async Task CreateAsync_ShouldAddRoomType_AndCallComplete()
        {
            var roomType = new RoomType
            {
                Id = Guid.NewGuid(),
                TypeName = "VIP",
                Description = "Top level room"
            };

            _mockRoomTypeRepo.Setup(r => r.AddAsync(roomType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _roomTypeService.CreateAsync(roomType);

            result.Should().BeEquivalentTo(roomType);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateRoomType_AndCallComplete()
        {
            var roomType = new RoomType
            {
                Id = Guid.NewGuid(),
                TypeName = "Updated Room",
                Description = "Updated Description"
            };

            _mockRoomTypeRepo.Setup(r => r.UpdateAsync(roomType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _roomTypeService.UpdateAsync(roomType);

            result.Should().BeEquivalentTo(roomType);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenSuccess()
        {
            var id = Guid.NewGuid();

            _mockRoomTypeRepo.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _roomTypeService.DeleteAsync(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNoChanges()
        {
            var id = Guid.NewGuid();

            _mockRoomTypeRepo.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var result = await _roomTypeService.DeleteAsync(id);

            result.Should().BeFalse();
        }
    }

}
