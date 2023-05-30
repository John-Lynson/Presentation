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
        var result = await productService.GetProductByIdAsync(1);

        // Assert
        Assert.Equal(testProduct, result);
        mockRepo.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

        var productService = new ProductService(mockRepo.Object);

        // Act
        var result = await productService.GetProductByIdAsync(2);

        // Assert
        Assert.Null(result);
        mockRepo.Verify(repo => repo.GetByIdAsync(2), Times.Once);
    }

    [Fact]
    public async Task GetProductByIdAsync_ThrowsException_WhenIdIsZero()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var productService = new ProductService(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => productService.GetProductByIdAsync(0));
    }

    [Fact]
    public async Task GetProductByIdAsync_ThrowsException_WhenIdIsNegative()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var productService = new ProductService(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => productService.GetProductByIdAsync(-1));
    }
}
