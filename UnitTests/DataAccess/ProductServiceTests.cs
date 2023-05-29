using Xunit;
using Moq;
using Core.Models;
using Core.Services;
using System.Threading.Tasks;
using Core.Repositories;
using DataAccess.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductByIdAsync_ReturnsExpectedProduct()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var testProduct = new Product { Id = 1, Name = "Test Product" };
        mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(testProduct);

        var productService = new ProductService(mockRepo.Object);

        // Act
        var result = await productService.GetProductByIdAsync(1); // Change to GetProductByIdAsync

        // Assert
        Assert.Equal(testProduct, result);
    }
}
