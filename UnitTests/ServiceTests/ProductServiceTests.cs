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
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo;
        private ProductService _productService;
        private Product _product;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepo.Object);
            _product = new Product(1, "Test Product", "This is a test product", "testurl", 10.0m, "TestCategory");
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(_product);

            // Act
            var product = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.Equal(1, product.Id);
        }

        // Other tests here...
    }
}