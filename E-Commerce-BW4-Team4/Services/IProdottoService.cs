using E_Commerce_BW4_Team4.Models;

namespace E_Commerce_BW4_Team4.Services
{
    public interface IProdottoService
    {
        IEnumerable<Prodotto> GetAllProducts();
        void Create(Prodotto prodotto);
        void Update(Prodotto prodotto);
        void Delete(IdProdotto);
        Prodotto GetById(IdProdotto);
    }
}
