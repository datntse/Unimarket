using Microsoft.AspNetCore.Mvc;

namespace Unimarket.API.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
