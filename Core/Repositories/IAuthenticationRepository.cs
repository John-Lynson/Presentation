using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationResult> RegisterAsync(User user, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> AuthenticateAsync(AuthenticateRequest model);
        Task<bool> VerifyTokenAsync(string token);
    }
}