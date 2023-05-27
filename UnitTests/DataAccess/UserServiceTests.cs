using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;

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
    }
}
