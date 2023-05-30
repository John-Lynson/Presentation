using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task AddItemAsync(string cartId, Product product, int quantity)
        {
            var cart = await _cartRepository.GetCartAsync(cartId) ?? new Cart { CartId = cartId };

            cart.AddItem(product, quantity);

            if (cart.CartId == null)
            {
                await _cartRepository.CreateAsync(cart);
            }
            else
            {
                await _cartRepository.UpdateAsync(cart);
            }
        }

        public async Task RemoveItemAsync(string cartId, Product product)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            if (cart != null)
            {
                cart.RemoveItem(product);
                await _cartRepository.UpdateAsync(cart);
            }
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            return await _cartRepository.GetCartAsync(cartId);
        }
    }
}
