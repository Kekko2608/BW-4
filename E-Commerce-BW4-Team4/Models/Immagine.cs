namespace E_Commerce_BW4_Team4.Models
{
    public class Immagine
    {
        public int IdImmagine { get; set; }

        public IFormFile ImmagineCopertina { get; set; }

        public IFormFile ImmagineSecondaria1 { get; set; }
        public IFormFile ImmagineSecondaria2 { get; set; }
        public IFormFile ImmagineSecondaria3 { get; set; }
    }
}
