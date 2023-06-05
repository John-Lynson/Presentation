using Xunit;
using Moq;
using Core.Services;
using System.Threading.Tasks;
using Core.Models;
using System.Collections.Generic;

public class ProductServiceTest
{
    private readonly Mock<IProductService> _mockProductService;

    public ProductServiceTest()
    {
        _mockProductService = new Mock<IProductService>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfProducts()
    {
        // Arrange
        var expectedProducts = new List<Product>
    {
        new Product(1, "Product 1", "Description 1", "ImageUrl 1", 10.0m, "Category 1"),
        new Product(2, "Product 2", "Description 2", "ImageUrl 2", 20.0m, "Category 2")
    };

        _mockProductService.Setup(svc => svc.GetAllAsync()).ReturnsAsync(expectedProducts);

        // Act
        var result = await _mockProductService.Object.GetAllAsync();

        // Assert
        Assert.Equal(expectedProducts, result);
    }

    // Add other tests for GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync, and GetProductsByCategoryAsync here
}
