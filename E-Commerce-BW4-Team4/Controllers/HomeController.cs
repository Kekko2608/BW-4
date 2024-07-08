using E_Commerce_BW4_Team4.Models;
using E_Commerce_BW4_Team4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace E_Commerce_BW4_Team4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdottoService _prodottoService;

        public HomeController(ILogger<HomeController> logger, IProdottoService prodottoService)
        {
            _logger = logger;
            _prodottoService = prodottoService;
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
        //-------------------------------------------------------------
        public IActionResult CreaProdotto()
        {
            return View(new Prodotto());
        }
        [HttpPost]
        public IActionResult CreaProdotto(Prodotto prodotto)
        {
            _prodottoService.Create(prodotto);
            return RedirectToAction("GestioneAmministratore");
        }

        public IActionResult GestioneAmministratore()
        {
            var Username = TempData["Username"] as string;
            ViewBag.Username = Username;
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
