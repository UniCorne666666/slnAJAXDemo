using Microsoft.AspNetCore.Mvc;
using prjAJAXDemo.Models;
using prjAJAXDemo.ViewModel;
using System.Diagnostics;

namespace prjAJAXDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DemoContext _demoContext;
        public HomeController(ILogger<HomeController> logger , DemoContext demoContext )
        {
            _logger = logger;
            _demoContext = demoContext;
        }

        public IActionResult Register() 
        {
            return View();
        }

        public IActionResult Member() 
        { 
            return View( _demoContext.Members);
        }
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Promise()
        {
            return View();
        }
        public IActionResult FirstAjax() 
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}