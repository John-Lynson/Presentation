using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
