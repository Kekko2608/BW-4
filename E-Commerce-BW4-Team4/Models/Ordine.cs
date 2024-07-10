namespace E_Commerce_BW4_Team4.Models
{
    public class Ordine
    {
        public int IdOrdine { get; set; }
        public ProdottoViewModel Prodotto { get; set; }
        public Carrello Carrello { get; set; }

        public int Quantita { get; set; }
    }
}
