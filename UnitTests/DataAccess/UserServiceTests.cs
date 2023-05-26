using Xunit;
using Moq;
using Core.Models;
using Core.Services;
using System.Threading.Tasks;
using Core.Repositories;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserByIdAsync_ReturnsExpectedUser()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var testUser = new User { Id = 1, FirstName = "Test User" };
        mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(testUser);

        var userService = new UserService(mockRepo.Object);

        // Act
        var result = await userService.GetUserByIdAsync(1);

        // Assert
        Assert.Equal(testUser, result);
    }
}