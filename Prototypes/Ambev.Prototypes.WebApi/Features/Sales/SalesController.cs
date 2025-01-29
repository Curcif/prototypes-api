using Microsoft.AspNetCore.Mvc;

namespace Ambev.Prototypes.WebApi.Features.Sales
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
