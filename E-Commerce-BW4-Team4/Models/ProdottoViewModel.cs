namespace E_Commerce_BW4_Team4.Models
{
    public class ProdottoViewModel : ProdottoCompleto
    {
     
        public string CoverImagePath { get; set; } // Aggiungi questa proprietà
        public string FirstImagePath { get; set; } // Aggiungi questa proprietà
        public string SecondImagePath { get; set; } // Aggiungi questa proprietà
        public string ThirdImagePath { get; set; } // Aggiungi questa proprietà


        public int Quantita { get; set; }
    }
}