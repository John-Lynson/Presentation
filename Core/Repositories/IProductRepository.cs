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
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByIdAsync(int id);
        // Definieer hier andere methoden die je nodig hebt
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    }
}
