using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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