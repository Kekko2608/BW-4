namespace E_Commerce_BW4_Team4.Models
{
    public class Carrello
    {
        public int IdCarrello { get; set; }
        public int Quantita { get; set; } = 1;
        public DateTime DatadelCarrello { get; set; } = DateTime.Now;

    }
}