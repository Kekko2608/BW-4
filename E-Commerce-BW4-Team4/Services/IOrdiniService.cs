using E_Commerce_BW4_Team4.Models;

namespace E_Commerce_BW4_Team4.Services
{
    public interface IOrdiniService
    {
        public (IEnumerable<OrdineCompleto>, OrdineCompleto) GetOrdiniCompleti();

        public void CreaOrdine(Ordine ordine, int idProdotto, int quantita);

        public void Delete(int idProdotto);

        public void DeleteAll();


    }
}
