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
        private readonly IOrdiniService _ordiniService;

        public HomeController(ILogger<HomeController> logger, IProdottoService prodottoService, IPiattaformaService piattaformaService, IGeneriService generiService, IOrdiniService ordiniService)
        {
            _logger = logger;
            _prodottoService = prodottoService;
            _piattaformaService = piattaformaService;
            _generiService = generiService;
            _ordiniService = ordiniService;
        }

        public IActionResult Index()
        {
            var prodotti = _prodottoService.GetAllProductsWithImages();
            return View(prodotti);
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
            return View(_prodottoService.GetAllProductsWithImages());
        }
        //----CRUD DEI PRODOTTI----
        public IActionResult CreaProdotto()
        {
            var TuttiIGeneri = _generiService.GetAllGeneri();
            var TutteLePiattaforme = _piattaformaService.GetAllPiattaforme();
            ViewBag.TuttiIGeneri = TuttiIGeneri;
            ViewBag.TutteLePiattaforme = TutteLePiattaforme;
            return View(new Prodotto());
        }
        [HttpPost]
        public IActionResult CreaProdotto(Prodotto prodotto, IFormFile ImageA, IFormFile ImageB, IFormFile ImageC, IFormFile ImageD)
        {
            if (prodotto == null)
                return BadRequest("Prodotto non valido.");

            int idGenereSelezionato = Convert.ToInt32(Request.Form["Genere"]);
            prodotto.Genere = idGenereSelezionato;

            int idPiattaformaSelezionata = Convert.ToInt32(Request.Form["Piattaforma"]);
            prodotto.Piattaforma = idPiattaformaSelezionata;

            _prodottoService.Create(prodotto);

            if (prodotto.IdProdotto > 0)
            {
                _prodottoService.SaveImages(prodotto.IdProdotto, ImageA, ImageB, ImageC, ImageD);
            }

            return RedirectToAction("GestioneAmministratore");
        }

      
        public IActionResult Modifica(int id)
        {
            var TuttiIGeneri = _generiService.GetAllGeneri();
            var TutteLePiattaforme = _piattaformaService.GetAllPiattaforme();
            ViewBag.TuttiIGeneri = TuttiIGeneri;
            ViewBag.TutteLePiattaforme = TutteLePiattaforme;

            var prodotto = _prodottoService.GetByIdForPC(id);
            ViewBag.GenereSelezionato = (int?)prodotto.Genere;
            ViewBag.PiattaformaSelezionata = (int?)prodotto.Piattaforma;
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modifica(ProdottoCompleto prodotto)
        {
            int idGenereSelezionato = Convert.ToInt32(Request.Form["Genere"]);
            prodotto.Genere = idGenereSelezionato;

            int idPiattaformaSelezionata = Convert.ToInt32(Request.Form["Piattaforma"]);
            prodotto.Piattaforma = idPiattaformaSelezionata;

            _prodottoService.Update(prodotto);
            return RedirectToAction(nameof(GestioneAmministratore));
        }
        public IActionResult Delete(int IdProdotto)
        {
            _prodottoService.Delete(IdProdotto);
            return RedirectToAction(nameof(GestioneAmministratore));
        }

        //---CRUD DEGLI ORDINI---
        public IActionResult CreaOrdine(Ordine ordine, int idProdotto, int quantita)
        {

            TempData["quantita"] = quantita;
            var quantità = TempData["quantita"];
            _ordiniService.CreaOrdine(ordine, idProdotto, quantita);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Ordini()
        {
            /*   var ordine = _ordiniService.GetAllOrdine();
             var ordineC = _ordiniService.GetOrdineCompleto();

               var tuttoOrdine = new OrdiniViewModel
               {
                   Ordini = ordine,
                   OrdineCompleto = ordineC
              }; */
            var (ordini, ordineCompleto) = _ordiniService.GetOrdiniCompleti();

            var viewModel = new OrdiniViewModel
            {
                Ordini = ordini,
                OrdineCompleto = ordineCompleto
            };

            return View(viewModel);

           // return View(tuttoOrdine);

        }
        public IActionResult ModifcaOrDeleteOrdine(Ordine ordine, int idOrdine, int quantita)
        {
            TempData["quantita"] = quantita;
            var quantità = TempData["quantita"];

            _ordiniService.ModifcaOrDelete(ordine, idOrdine, quantita);
            return RedirectToAction(nameof(Ordini));
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
