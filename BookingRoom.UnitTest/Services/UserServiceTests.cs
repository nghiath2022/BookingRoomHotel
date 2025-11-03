using BookingRoom.DTOs;
using BookingRoom.Interfaces;
using BookingRoom.Models;
using BookingRoom.Services;
using FluentAssertions;
using Moq;

namespace BookingRoom.UnitTest.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public UserServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);

            _userService = new UserService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUserList()
        {
            var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), FullName = "Alice" },
            new User { Id = Guid.NewGuid(), FullName = "Bob" }
        };

            _mockUserRepo.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _userService.GetAllUsersAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, FullName = "Charlie" };

            _mockUserRepo.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);

            var result = await _userService.GetUserByIdAsync(userId);

            result.Should().NotBeNull();
            result!.FullName.Should().Be("Charlie");
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnCreatedUser_AndCallComplete()
        {
            var user = new User { Id = Guid.NewGuid(), FullName = "Daisy" };

            _mockUserRepo.Setup(r => r.CreateUserAsync(user)).ReturnsAsync(user);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _userService.CreateUserAsync(user);

            result.Should().BeEquivalentTo(user);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_AndCallComplete()
        {
            var user = new User { Id = Guid.NewGuid(), FullName = "Eve" };

            _mockUserRepo.Setup(r => r.UpdateUserAsync(user)).ReturnsAsync(user);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _userService.UpdateUserAsync(user);

            result.Should().BeEquivalentTo(user);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenDeleted_AndCallComplete()
        {
            var id = Guid.NewGuid();

            _mockUserRepo.Setup(r => r.DeleteUserAsync(id)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _userService.DeleteUserAsync(id);

            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenNotDeleted_AndNotCallComplete()
        {
            var id = Guid.NewGuid();

            _mockUserRepo.Setup(r => r.DeleteUserAsync(id)).ReturnsAsync(false);

            var result = await _userService.DeleteUserAsync(id);

            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnUser()
        {
            var email = "test@mail.com";
            var user = new User { Email = email, FullName = "Tester" };

            _mockUserRepo.Setup(r => r.GetUserByEmailAsync(email)).ReturnsAsync(user);

            var result = await _userService.GetUserByEmailAsync(email);

            result.Should().NotBeNull();
            result!.Email.Should().Be(email);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnCreatedUser_AndCallComplete()
        {
            var request = new RegisterRequest
            {
                Email = "new@mail.com",
                Password = "123456",
                FullName = "New User",
                RoleName = "User"
            };

            var role = new Role { Id = Guid.NewGuid(), Name = "User" };

            _mockUserRepo.Setup(r => r.GetUserByEmailAsync(request.Email)).ReturnsAsync((User?)null);
            _mockUserRepo.Setup(r => r.GetRoleByNameAsync("User")).ReturnsAsync(role);
            _mockUserRepo.Setup(r => r.CreateUserAsync(It.IsAny<User>())).ReturnsAsync((User user) => user);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var result = await _userService.RegisterUserAsync(request);

            result.Should().NotBeNull();
            result.Email.Should().Be("new@mail.com");
            result.RoleId.Should().Be(role.Id);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
