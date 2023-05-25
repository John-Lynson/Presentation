using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        // Definieer hier andere methoden die je nodig hebt
    }
}