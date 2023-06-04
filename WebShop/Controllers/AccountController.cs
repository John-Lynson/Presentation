using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Models;
using DataAccess.Repositories;
using WebShop.ViewModels;
using Core.Repositories;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Password == null)
            {
                return BadRequest("Vul een wachtwoord in");
            }

            var user = new User(
                id: Guid.NewGuid().ToString(),
                email: model.Email ?? string.Empty,
                passwordHash: model.Password,
                firstName: model.FirstName ?? string.Empty,
                lastName: model.LastName ?? string.Empty
            );

            // Voeg de logica toe om de gebruiker toe te voegen aan de database met behulp van ADO.NET

            _userRepository.CreateAsync(user);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUserByEmailAsync(model.Email);

            if (user == null || user.PasswordHash != model.Password)
            {
                return Unauthorized();
            }

            // Voeg de logica toe om de gebruiker in te loggen

            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            // Voeg de logica toe om de gebruiker uit te loggen

            return Ok();
        }
    }
}
