using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Core.Services;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
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
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _userRepository.CreateAsync(user);
            string token = GenerateJwtToken(user);
            return new AuthenticationResult { IsAuthenticated = true, Token = token };
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password).Equals(PasswordVerificationResult.Success))
            {
                return new AuthenticationResult { IsAuthenticated = false };
            }
            string token = GenerateJwtToken(user);
            return new AuthenticationResult { IsAuthenticated = true, Token = token };
        }

        public async Task<AuthenticationResult> AuthenticateAsync(AuthenticateRequest model)
        {
            User user = await _userRepository.GetUserByEmailAsync(model.Email);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password).Equals(PasswordVerificationResult.Success))
            {
                return new AuthenticationResult { IsAuthenticated = false };
            }

            string token = GenerateJwtToken(user);

            return new AuthenticationResult { IsAuthenticated = true, Token = token };
        }

        public async Task<bool> VerifyTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    return await Task.FromResult(false);
                }

                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }


        private string GenerateJwtToken(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || user.Id == null)
            {
                throw new ArgumentNullException("User, user email, or user id cannot be null.");
            }

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
