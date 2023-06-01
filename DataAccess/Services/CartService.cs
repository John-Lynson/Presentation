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
            // Implementatie voor het verwijderen van een item uit de cart
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            return await _cartRepository.GetCartAsync(cartId);
        }
    }
}