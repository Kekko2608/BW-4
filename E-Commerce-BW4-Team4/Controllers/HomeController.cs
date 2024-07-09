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
        private readonly IPiattaformaService _piattaformaService;
        private readonly IGeneriService _generiService;

        public HomeController(ILogger<HomeController> logger, IProdottoService prodottoService, IPiattaformaService piattaformaService, IGeneriService generiService)
        {
            _logger = logger;
            _prodottoService = prodottoService;
            _piattaformaService = piattaformaService;
            _generiService = generiService;
        }

        public IActionResult Index()
        {
            return View(_prodottoService.GetAllProducts());
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
            var TuttiIGeneri = _generiService.GetAllGeneri();
            var TutteLePiattaforme = _piattaformaService.GetAllPiattaforme();
            ViewBag.TuttiIGeneri = TuttiIGeneri;
            ViewBag.TutteLePiattaforme = TutteLePiattaforme;
            return View(new Prodotto());
        }
        [HttpPost]
        public IActionResult CreaProdotto(Prodotto prodotto)
        {
            int idGenereSelezionato = Convert.ToInt32(Request.Form["Genere"]);

            prodotto.Genere = idGenereSelezionato;

            int idPiattaformaSelezionata = Convert.ToInt32(Request.Form["Piattaforma"]);

            prodotto.Piattaforma = idPiattaformaSelezionata;

            _prodottoService.Create(prodotto);
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
