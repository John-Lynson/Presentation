using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;
using Xunit;
using System.Threading.Tasks;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserByIdAsync_ReturnsExpectedUser()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var testUser = new User { Id = "1", FirstName = "Test User" }; // Id is now a string
        mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(testUser); // GetByIdAsync now takes a string argument

        var userService = new UserService(mockRepo.Object);

        // Act
        var result = await userService.GetUserByIdAsync("1"); // GetUserByIdAsync now takes a string argument

        // Assert
        Assert.Equal(testUser, result);
        mockRepo.Verify(repo => repo.GetByIdAsync("1"), Times.Once);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

        var userService = new UserService(mockRepo.Object);

        // Act
        var result = await userService.GetUserByIdAsync("2");

        // Assert
        Assert.Null(result);
        mockRepo.Verify(repo => repo.GetByIdAsync("2"), Times.Once);
    }

    [Fact]
    public async Task GetUserByIdAsync_ThrowsException_WhenIdIsNull()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var userService = new UserService(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => userService.GetUserByIdAsync(null));
    }

    [Fact]
    public async Task GetUserByIdAsync_ThrowsException_WhenIdIsEmpty()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var userService = new UserService(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => userService.GetUserByIdAsync(""));
    }
}
