using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Core.Models;
using Core.Repositories;

namespace Core.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> RegisterAsync(User user, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> AuthenticateAsync(AuthenticateRequest model);
        bool VerifyToken(string token);
    }


    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly string _jwtKey;

        public AuthenticationService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, string jwtKey)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtKey = jwtKey;
        }

        public async Task<AuthenticationResult> RegisterAsync(User user, string password)
        {
            // Hash het wachtwoord
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            // Gebruiker toevoegen aan repository
            await _userRepository.AddUserAsync(user);

            // Genereer JWT token voor de nieuwe gebruiker
            string token = GenerateJwtToken(user);

            return new AuthenticationResult { IsAuthenticated = true, Token = token };
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
            {
                // Incorrecte email of wachtwoord
                return new AuthenticationResult { IsAuthenticated = false };
            }

            // Genereer JWT token voor de ingelogde gebruiker
            string token = GenerateJwtToken(user);

            return new AuthenticationResult { IsAuthenticated = true, Token = token };
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string email, string password)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)

            {
                // Incorrecte email of wachtwoord
                return new AuthenticationResult { IsAuthenticated = false };
            }

            // Genereer JWT token voor de geauthenticeerde gebruiker
            string token = GenerateJwtToken(user);

            return new AuthenticationResult { IsAuthenticated = true, Token = token };
        }

        public async Task<AuthenticationResult> AuthenticateAsync(AuthenticateRequest model)
        {
            return await AuthenticateAsync(model.Email, model.Password);
        }

        public bool VerifyToken(string token)
        {
            // TODO: Implementeer de logica om het token te verifiëren
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
