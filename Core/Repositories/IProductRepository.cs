using Core.Models;
using Core.Repositories;

namespace Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product); 
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    }
}
