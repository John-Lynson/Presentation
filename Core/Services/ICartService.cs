using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICartService
    {
        Task AddItemAsync(string cartId, Product product, int quantity);
        Task RemoveItemAsync(string cartId, Product product);
        Task<Cart> GetCartAsync(string cartId);
    }
}
