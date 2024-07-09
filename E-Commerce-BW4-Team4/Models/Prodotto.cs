namespace E_Commerce_BW4_Team4.Models
{
    public class Prodotto
    {
        public int IdProdotto { get; set; }
        public string NomeProdotto { get; set; }
        public string DescrizioneProdotto { get; set; }
        public string Brand { get; set; }
        public string PEGI { get; set; }
        public string CodiceABarre { get; set; }
        public bool Disponibilita { get; set; }
        public decimal Prezzo { get; set; }
        public int Piattaforma { get; set; }
        public int Genere { get; set; }
        public ICollection<Ordine> Ordini { get; set; }
    }
}
