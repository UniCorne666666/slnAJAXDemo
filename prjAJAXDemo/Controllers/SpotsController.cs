using Microsoft.AspNetCore.Mvc;

namespace prjAJAXDemo.Controllers
{
    public class SpotsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
