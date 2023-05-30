using Core.Models;
using Core.Repositories;
using Core.Services;
using System;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task AddItemAsync(string cartId, Product product, int quantity)
        {
            // Get the cart from the repository
            var cart = await _cartRepository.GetCartAsync(cartId);

            // If the cart does not exist, create a new one
            if (cart == null)
            {
                cart = new Cart { CartId = cartId };
                await _cartRepository.CreateAsync(cart);
            }

            // Add the item to the cart
            cart.AddItem(product, quantity);

            // Update the cart in the repository
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task RemoveItemAsync(string cartId, Product product)
        {
            // Get the cart from the repository
            var cart = await _cartRepository.GetCartAsync(cartId);

            // If the cart does not exist, then there's nothing to remove
            if (cart == null)
            {
                return;
            }

            // Remove the item from the cart
            cart.RemoveItem(product);

            // Update the cart in the repository
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            return await _cartRepository.GetCartAsync(cartId);
        }
    }
}
