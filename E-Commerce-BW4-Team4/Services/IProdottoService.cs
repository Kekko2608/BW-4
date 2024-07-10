using E_Commerce_BW4_Team4.Models;


namespace E_Commerce_BW4_Team4.Services
{
    public interface IProdottoService
    {
        IEnumerable<ProdottoCompleto> GetAllProducts();
        IEnumerable<ProdottoViewModel> GetAllProductsWithImages();
        void Create(Prodotto prodotto);
        void Update(Prodotto prodotto);
        void Delete(int IdProdotto);
        Prodotto GetById(int IdProdotto);
        ProdottoCompleto GetByIdForPC(int IdProdottoForPC);

        void SaveImages(int idProdotto, IFormFile ImageA, IFormFile ImageB, IFormFile ImageC, IFormFile ImageD);
    }
}
