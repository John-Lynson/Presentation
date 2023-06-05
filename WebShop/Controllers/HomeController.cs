using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebShop.ViewModels;
using Core.Services;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Loading home page");
        var products = await _productService.GetAllAsync();
        _logger.LogInformation("Home page loaded successfully");
        return View(products);
    }

    public async Task<IActionResult> Product(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Cart()
    {
        // Assuming the user's cart ID is accessible and stored in variable "cartId"
        string cartId = "some-cart-id";
        var cart = await _cartService.GetCartAsync(cartId);
        return View(cart.CartItems);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

