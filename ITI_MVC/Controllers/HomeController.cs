using System.Diagnostics;
using ITI_MVC.DatabaseInitializer;
using ITI_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (DbInitializer.AdminCreated)
            {
                ViewBag.Message = "? Admin user has been created successfully.";
            }
            else
            {
                ViewBag.Message = "";
            }

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
