using Xunit;
using Moq;
using Core.Models;
using Core.Repositories;
using Core.Services;
using System.Threading.Tasks;
using DataAccess.Services;

namespace UnitTests
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;
        private readonly Mock<IOrderRepository> _mockOrderRepo;

        public OrderServiceTests()
        {
            _mockOrderRepo = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockOrderRepo.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnTrue_WhenOrderIsAddedSuccessfully()
        {
            var order = new Order.Builder().WithId(1).Build();
            _mockOrderRepo.Setup(repo => repo.CreateAsync(order)).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.CreateOrderAsync(order);

            // Assert
            Assert.True(result);
            _mockOrderRepo.Verify(repo => repo.CreateAsync(order), Times.Once);
        }

        // ... voeg hier je andere tests toe ...
    }
}
