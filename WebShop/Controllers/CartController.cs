using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var cart = await _cartService.GetCartAsync(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("{cartId}/items")]
        public async Task<IActionResult> AddItem(string cartId, [FromBody] Product product, [FromQuery] int quantity)
        {
            await _cartService.AddItemAsync(cartId, product, quantity);

            return NoContent();
        }

        [HttpDelete("{cartId}/items")]
        public async Task<IActionResult> RemoveItem(string cartId, [FromBody] Product product)
        {
            await _cartService.RemoveItemAsync(cartId, product);

            return NoContent();
        }
    }
}
