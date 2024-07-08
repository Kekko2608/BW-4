using E_Commerce_BW4_Team4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_BW4_Team4.Controllers
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
            return View();
        }
        [HttpGet]
        public IActionResult Amministratore()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Amministratore(string Username)
        {
            TempData["Username"] = Username;
            return RedirectToAction("GestioneAmministratore");
        }

        public IActionResult GestioneAmministratore()
        {
            var Username = TempData["Username"] as string;
            ViewBag.Username = Username;
            return View();
        }

        public IActionResult Carrello()
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
