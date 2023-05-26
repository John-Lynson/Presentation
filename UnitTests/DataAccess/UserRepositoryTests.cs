using Core.Models;
using Core.Repositories;
using DataAccess.Repositories;
using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccess;

namespace WebShop.Tests
{
    public class UserRepositoryTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private IUserRepository _userRepository;
        private List<User> _users;

        public UserRepositoryTests()
        {
            _users = new List<User>
           {
        new User { Id = "1", Email = "test1@example.com", PasswordHash = "testHash1" },
        new User { Id = "2", Email = "test2@example.com", PasswordHash = "testHash2" }
            };

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(_users.AsQueryable().Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(_users.AsQueryable().Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(_users.AsQueryable().ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(_users.AsQueryable().GetEnumerator());

            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            _userRepository = new UserRepository(_mockContext.Object);
        }

        [Fact]
        public async void AddUserAsync_AddsUserCorrectly()
        {
            var newUser = new User { Id = "3", Email = "test3@example.com", PasswordHash = "testHash3" };
            await _userRepository.CreateAsync(newUser);
            _mockContext.Verify(m => m.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void GetAllAsync_ReturnsAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            Assert.Equal(_users, users);
        }

        [Fact]
        public async void GetUserByEmailAsync_ReturnsCorrectUser()
        {
            var email = "test1@example.com";
            var user = await _userRepository.GetUserByEmailAsync(email);
            Assert.Equal(_users[0], user);
        }

        [Fact]
        public async Task UpdateAsync_ChangesUser()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var testUser = new User { Id = "1", Email = "test@example.com", PasswordHash = "12345" };
            var repository = new UserRepository(mockContext.Object);

            // Act
            testUser.Email = "changed@example.com";
            await repository.UpdateAsync(testUser);

            // Assert
            mockSet.Verify(m => m.Update(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default(CancellationToken)), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_RemovesUser()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var testUser = new User { Id = "1", Email = "test@example.com", PasswordHash = "12345" };
            var repository = new UserRepository(mockContext.Object);

            // Act
            await repository.DeleteAsync(testUser);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default(CancellationToken)), Times.Once());
        }

        [Fact]
        public async Task GetUserByEmailAsync_ReturnsNullForNonExistentEmail()
        {
            // Arrange
            var data = new List<User>
    {
        new User { Id = "1", Email = "test1@example.com", PasswordHash = "12345" },
        new User { Id = "2", Email = "test2@example.com", PasswordHash = "23456" },
        new User { Id = "3", Email = "test3@example.com", PasswordHash = "34567" },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var repository = new UserRepository(mockContext.Object);

            // Act
            var user = await repository.GetUserByEmailAsync("nonexistent@example.com");

            // Assert
            Assert.Null(user);
        }
    }
}
