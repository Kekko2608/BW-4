﻿namespace E_Commerce_BW4_Team4.Models
{
    public class OrdineCompleto : ProdottoViewModel
    {
        public int IdOrdine { get; set; }
        public decimal PrezzoTotale { get; set; }

        public decimal QuantitaTotale { get; set; }
    }
}