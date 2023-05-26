using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task CreateAsync(User user); // renamed from AddUserAsync
        Task UpdateAsync(User user);
        Task DeleteAsync(User user); // renamed from RemoveAsync
        Task<User> GetUserByEmailAsync(string email);
    }
}
