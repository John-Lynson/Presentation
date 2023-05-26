using Xunit;
using Moq;
using Core.Models;
using Core.Services;
using System.Threading.Tasks;
using Core.Repositories;
using DataAccess.Services;

public class OrderServiceTests
{
    [Fact]
    public async Task GetOrderByIdAsync_ReturnsExpectedOrder()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var testOrder = new Order { Id = 1, UserId = 1, OrderDate = DateTime.Now };
        mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(testOrder);

        var orderService = new OrderService(mockRepo.Object);

        // Act
        var result = await orderService.GetOrderByIdAsync(1);

        // Assert
        Assert.Equal(testOrder, result);
    }
}