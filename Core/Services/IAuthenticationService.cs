using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;
using Core.Services;

    namespace Core.Services
    {
        public interface IAuthenticationService
        {
            Task<AuthenticationResult> RegisterAsync(User user, string password);
            Task<AuthenticationResult> LoginAsync(string email, string password);
            Task<AuthenticationResult> AuthenticateAsync(AuthenticateRequest model);
            Task<bool> VerifyTokenAsync(string token);
        }
    }
