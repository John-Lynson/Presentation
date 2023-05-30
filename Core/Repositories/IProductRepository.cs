using Core.Models;
using Core.Repositories;

namespace Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetProductAsync(int productId);
        Task CreateAsync(Product product); // make sure this method is in the interface
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product); // make sure this method is in the interface
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    }
}
