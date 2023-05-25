using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Core.Models;
using Core.Repositories;


namespace Core.Services
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(User user, string password);
        Task<string> LoginAsync(string email, string password);
        bool VerifyToken(string token);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtKey;

        public AuthenticationService(IUserRepository userRepository, string jwtKey)
        {
            _userRepository = userRepository;
            _jwtKey = jwtKey;
        }

        public async Task<string> RegisterAsync(User user, string password)
        {
            // TODO: Implementeer de registratielogica
            throw new NotImplementedException();
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            // TODO: Implementeer de loginlogica
            throw new NotImplementedException();
        }

        public bool VerifyToken(string token)
        {
            // TODO: Implementeer de tokenverificatielogica
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
