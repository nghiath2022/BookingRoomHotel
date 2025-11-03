using BookingRoom.Interfaces;
using BookingRoom.Models;
using BookingRoom.Services;
using FluentAssertions;
using Moq;

namespace BookingRoom.UnitTest.Services
{
    public class RoomServiceTests
    {
        private readonly RoomService _roomService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRoomRepository> _mockRoomRepo;

        public RoomServiceTests()
        {
            _mockRoomRepo = new Mock<IRoomRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(u => u.Rooms).Returns(_mockRoomRepo.Object);

            _roomService = new RoomService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllRoomsAsync_ShouldReturnListOfRooms()
        {
            // Arrange
            var rooms = new List<Room>
        {
            new Room { Id = Guid.NewGuid(), Name = "Room A", Price = 100 },
            new Room { Id = Guid.NewGuid(), Name = "Room B", Price = 200 }
        };

            _mockRoomRepo.Setup(r => r.GetAllRoomsAsync()).ReturnsAsync(rooms);

            // Act
            var result = await _roomService.GetAllRoomsAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetRoomByIdAsync_ShouldReturnRoom_WhenFound()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var room = new Room { Id = roomId, Name = "VIP Room" };

            _mockRoomRepo.Setup(r => r.GetRoomByIdAsync(roomId)).ReturnsAsync(room);

            // Act
            var result = await _roomService.GetRoomByIdAsync(roomId);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("VIP Room");
        }

        [Fact]
        public async Task CreateRoomAsync_ShouldReturnCreatedRoom_AndCallComplete()
        {
            // Arrange
            var newRoom = new Room { Id = Guid.NewGuid(), Name = "New Room", Price = 150 };

            _mockRoomRepo.Setup(r => r.CreateRoomAsync(newRoom)).ReturnsAsync(newRoom);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1); // nếu Task<int>

            // Act
            var result = await _roomService.CreateRoomAsync(newRoom);

            // Assert
            result.Should().BeEquivalentTo(newRoom);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateRoomAsync_ShouldReturnUpdatedRoom_AndCallComplete()
        {
            // Arrange
            var room = new Room { Id = Guid.NewGuid(), Name = "Updated Room", Price = 220 };

            _mockRoomRepo.Setup(r => r.UpdateRoomAsync(room)).ReturnsAsync(room);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _roomService.UpdateRoomAsync(room);

            // Assert
            result.Should().BeEquivalentTo(room);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteRoomAsync_ShouldReturnTrue_AndCallComplete_WhenDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockRoomRepo.Setup(r => r.DeleteRoomAsync(id)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _roomService.DeleteRoomAsync(id);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteRoomAsync_ShouldReturnFalse_AndNotCallComplete_WhenNotDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockRoomRepo.Setup(r => r.DeleteRoomAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _roomService.DeleteRoomAsync(id);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
