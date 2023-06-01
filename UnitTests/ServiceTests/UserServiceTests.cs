using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserServiceTests
{
    private Mock<IUserRepository> _mockRepo;
    private UserService _userService;
    private User _user;

    public UserServiceTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _userService = new UserService(_mockRepo.Object);
        _user = new User("1", "Test User", "passwordHash", "Test", "User");

    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(_user);

        // Act
        var user = await _userService.GetUserByIdAsync("1");

        // Assert
        Assert.Equal("1", user.Id);
    }

    // Other tests here...
}