using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        Task CreateAsync(Cart cart);
        Task UpdateAsync(Cart cart);
    }
}