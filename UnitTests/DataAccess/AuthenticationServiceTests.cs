using Xunit;
using Moq;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Identity;
using DataAccess.Services;
using System.Threading.Tasks;

namespace UnitTests
{
    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService _authService;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;

        public AuthenticationServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _authService = new AuthenticationService(_mockUserRepo.Object, _mockPasswordHasher.Object, "test_jwt_key");
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnAuthenticated_WhenUserIsAddedSuccessfully()
        {
            // Arrange
            var user = new User { Email = "test@test.com" };
            var password = "testPassword";
            _mockUserRepo.Setup(repo => repo.CreateAsync(user)).Returns(Task.CompletedTask);
            _mockPasswordHasher.Setup(hasher => hasher.HashPassword(user, password)).Returns("hashedPassword");

            // Act
            var result = await _authService.RegisterAsync(user, password);

            // Assert
            Assert.True(result.IsAuthenticated);
            Assert.NotNull(result.Token);
            _mockUserRepo.Verify(repo => repo.CreateAsync(user), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.HashPassword(user, password), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNotAuthenticated_WhenUserDoesNotExist()
        {
            // Arrange
            var email = "nonexistent@test.com";
            var password = "testPassword";
            _mockUserRepo.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            // Act
            var result = await _authService.LoginAsync(email, password);

            // Assert
            Assert.False(result.IsAuthenticated);
            _mockUserRepo.Verify(repo => repo.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnAuthenticated_WhenPasswordIsCorrect()
        {
            // Arrange
            var user = new User { Email = "test@test.com", PasswordHash = "hashedPassword" };
            var password = "testPassword";
            _mockUserRepo.Setup(repo => repo.GetUserByEmailAsync(user.Email)).Returns(Task.FromResult(user));
            _mockPasswordHasher.Setup(hasher => hasher.VerifyHashedPassword(user, user.PasswordHash, password)).Returns(PasswordVerificationResult.Success);

            // Act
            var result = await _authService.LoginAsync(user.Email, password);

            // Assert
            Assert.True(result.IsAuthenticated);
            Assert.NotNull(result.Token);
            _mockUserRepo.Verify(repo => repo.GetUserByEmailAsync(user.Email), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyHashedPassword(user, user.PasswordHash, password), Times.Once);
        }

        [Fact]
            public async Task AuthenticateAsync_ShouldReturnAuthenticated_WhenUserCredentialsAreCorrect()
        {
            // Arrange
            var user = new User { Email = "test@test.com", PasswordHash = "hashedPassword" };
            var authRequest = new AuthenticateRequest(user.Email, "testPassword");
            _mockUserRepo.Setup(repo => repo.GetUserByEmailAsync(user.Email)).Returns(Task.FromResult(user));
            _mockPasswordHasher.Setup(hasher => hasher.VerifyHashedPassword(user, user.PasswordHash, "testPassword")).Returns(PasswordVerificationResult.Success);

            // Act
            var result = await _authService.AuthenticateAsync(authRequest);

            // Assert
            Assert.True(result.IsAuthenticated);
            Assert.NotNull(result.Token);
            _mockUserRepo.Verify(repo => repo.GetUserByEmailAsync(user.Email), Times.Once);
        }
    }
}
