namespace E_Commerce_BW4_Team4.Models
{
    public class ProdottoViewModel
    {
        public int IdProdotto { get; set; }
        public string NomeProdotto { get; set; }
        public string DescrizioneProdotto { get; set; }
        public bool Disponibilita { get; set; }
        public decimal Prezzo { get; set; }
        public string CoverImagePath { get; set; } // Aggiungi questa proprietà
    }
}