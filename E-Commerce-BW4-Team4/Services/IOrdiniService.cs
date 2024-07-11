﻿using E_Commerce_BW4_Team4.Models;

namespace E_Commerce_BW4_Team4.Services
{
    public interface IOrdiniService
    {
         (IEnumerable<OrdineCompleto>, OrdineCompleto) GetOrdiniCompleti();

        void CreaOrdine(Ordine ordine, int idProdotto, int quantita);

        void ModificaCarrello(Ordine ordine, int quantita);

        void ModifcaOrDelete(Ordine Ordine, int idOrdine, int quantita);

        void DeleteAll();


    }
}
