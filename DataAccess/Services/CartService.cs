using Core.Models;
using Core.Repositories;
using Core.Services;
using System.Threading.Tasks;

namespace DataAccess.Services
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
            // Implementatie voor het toevoegen van een item aan de cart
        }

        public async Task RemoveItemAsync(string cartId, Product product)
        {
            // Haal de huidige cart op
            var cart = await _cartRepository.GetCartAsync(cartId);

            if (cart != null)
            {
                // Zoek het item dat overeenkomt met het product in de cart
                var itemToRemove = cart.CartItems.FirstOrDefault(item => item.Product.Id == product.Id);

                if (itemToRemove != null)
                {
                    // Verwijder het item uit de cart
                    cart.CartItems.Remove(itemToRemove);

                    // Update de cart in de repository
                    await _cartRepository.UpdateAsync(cart);
                }
            }
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            return await _cartRepository.GetCartAsync(cartId);
        }
    }
}