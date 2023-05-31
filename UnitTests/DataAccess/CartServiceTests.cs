using Xunit;
using Moq;
using Core.Services;
using Core.Repositories;
using Core.Models;
using System.Threading.Tasks;
using DataAccess.Services;

namespace Tests
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock = new Mock<ICartRepository>();
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly ICartService _cartService;

        public CartServiceTests()
        {
            _cartService = new CartService(_cartRepositoryMock.Object, _productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCartAsync_ShouldReturnCart_WhenExists()
        {
            var testCartId = "testCartId";
            var testCart = new Cart.Builder().WithCartId(testCartId).Build();
            _cartRepositoryMock.Setup(repo => repo.GetCartAsync(testCartId)).ReturnsAsync(testCart);

            var result = await _cartService.GetCartAsync(testCartId);

            Assert.Equal(testCart, result);
            _cartRepositoryMock.Verify(repo => repo.GetCartAsync(testCartId), Times.Once());
        }


        [Fact]
        public async Task AddItemAsync_ShouldCallCorrectMethods()
        {
            var testCartId = "testCartId";
            var testProduct = new Product.Builder().WithId(1).WithName("Test product").Build();
            var testQuantity = 1;
            var testCart = new Cart.Builder().WithCartId(testCartId).Build();
            // ...
        }

        [Fact]
        public async Task RemoveItemAsync_ShouldCallCorrectMethods()
        {
            var testCartId = "testCartId";
            var testProduct = new Product.Builder().WithId(1).WithName("Test product").Build();
            var testCart = new Cart.Builder().WithCartId(testCartId).Build();
            // ...
        }
    }
}
