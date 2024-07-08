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
        public int IdPiattaforma { get; set; }
        public int IdGenere { get; set; }
        public int IdImmagini { get; set; }
        public Piattaforma Piattaforma { get; set; }
        public Genere Genere { get; set; }
        public Immagine Immagine { get; set; }
        public ICollection<Ordine> Ordini { get; set; }
    }
}
