using Core.Models;
using Core.Repositories;
using DataAccess.Services;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.DataAccess
{
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _mockRepo;
        private OrderService _orderService;
        private Order _order;

        public OrderServiceTests()
        {
            _mockRepo = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockRepo.Object);
            _order = new Order(1, "1", DateTime.Now);
        }

        [Fact]
        public async Task CreateOrderAsync_ReturnsTrue_WhenOrderCreationSucceeds()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            // Act
            var success = await _orderService.CreateOrderAsync(_order);

            // Assert
            Assert.True(success);
        }

        // Other tests here...
    }
}