using E_Commerce_BW4_Team4.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace E_Commerce_BW4_Team4.Services
{
    public class OrdiniService : SqlServerServiceBase, IOrdiniService
    {
        public OrdiniService(IConfiguration config) : base(config)
        {
        }

        public OrdineCompleto Create(DbDataReader reader)
        {
            return new OrdineCompleto
            {
                NomeProdotto = reader.GetString(0),
                Brand = reader.GetString(1),
                NomePiattaforma = reader.GetString(2),
                QuantitaTotale = reader.GetInt32(3),
                PrezzoTotale = reader.GetDecimal(4),
                IdProdotto = reader.GetInt32(5),
                CoverImagePath = $"/Images/{reader.GetInt32(5)}a.jpg",
                IdOrdine = reader.GetInt32(6),
            };
        }
        public (IEnumerable<OrdineCompleto>, OrdineCompleto) GetOrdiniCompleti()
        {
            var ordini = new List<OrdineCompleto>();
            var ordineCompleto = new OrdineCompleto();
            var queryOrdini = @"
        SELECT p.NomeProdotto, p.Brand, pt.NomePiattaforma, 
               SUM(o.Quantita) as QuantitaTotale, 
               SUM(p.Prezzo * o.Quantita) as PrezzoTotale, 
               p.IdProdotto, o.IdOrdine
        FROM Ordini as o 
        JOIN Carrello as c ON o.IdCarrello = c.IdCarrello 
        JOIN Prodotti as p ON o.IdProdotti = p.IdProdotto 
        JOIN Generi as g ON p.IdGenere = g.IdGenere 
        JOIN Piattaforme as pt ON p.IdPiattaforma = pt.IdPiattaforma 
        WHERE c.IdCarrello = 1 
        GROUP BY p.NomeProdotto, p.Brand, pt.NomePiattaforma, p.IdProdotto, o.IdOrdine";

            var queryOrdineCompleto = @"
        SELECT ISNULL (SUM(o.Quantita),0) as QuantitaTotale, 
               ISNULL (SUM(p.Prezzo * o.Quantita),0) as PrezzoTotale 
        FROM Ordini as o 
        JOIN Carrello as c ON o.IdCarrello = c.IdCarrello 
        JOIN Prodotti as p ON o.IdProdotti = p.IdProdotto 
        WHERE c.IdCarrello = 1";

            using (var conn = GetConnection())
            {
                conn.Open();

                // Esegui la query per gli ordini
                using (var cmd1 = GetCommand(queryOrdini))
                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        ordini.Add(Create(reader1));
                    }
                }

                // Esegui la query per l'ordine completo
                using (var cmd2 = GetCommand(queryOrdineCompleto))
                using (var reader2 = cmd2.ExecuteReader())
                {
                    if (reader2.Read())
                    {
                        ordineCompleto.QuantitaTotale = reader2.GetInt32(0);
                        ordineCompleto.PrezzoTotale =  reader2.GetDecimal(1);
                    }
                }
            }

            return (ordini, ordineCompleto);
        }
        
        public void CreaOrdine(Ordine ordine, int idProdotto, int Quantita)
        {
            var query = "INSERT INTO Ordini (IdProdotti, IdCarrello, Quantita) VALUES(@IdProdotti, 1, @Quantita)";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@Quantita", Quantita));
            cmd.Parameters.Add(new SqlParameter("@IdProdotti", idProdotto));
            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteNonQuery();

            if (result != 1)
                throw new Exception("Creazione non completata");
        }
        

        public void ModificaCarrello(Ordine ordine, int quantita)
        {
            var query = "UPDATE Ordini SET Quantita = @quantita WHERE IdOrdine = @IdOrdine";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@IdProdotto", quantita));
 

            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteNonQuery();

            if (result != 1)
                throw new Exception("Creazione non completata");
        }
 

        public void DeleteAll()
        {
            var query = "DELETE FROM Ordini";
            var cmd = GetCommand(query);

            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteNonQuery();

            if (result != 1)
                throw new Exception("Creazione non completata");
        }


        public void ModifcaOrDelete(Ordine Ordine, int idOrdine, int quantita)
        {
          if(quantita >= 1)
            {
                var query = "UPDATE Ordini SET Quantita = @quantita WHERE IdOrdine = @IdOrdine";
                var cmd = GetCommand(query);
                cmd.Parameters.Add(new SqlParameter("@IdOrdine", idOrdine));
                cmd.Parameters.Add(new SqlParameter("@quantita", quantita));
                using var conn = GetConnection();
                conn.Open();
                var result = cmd.ExecuteNonQuery();

                if (result != 1)
                    throw new Exception("Modifica non completata");
            } 
            else if(quantita == 0) 
            {
                    var query = "DELETE FROM Ordini WHERE IdOrdine = @IdOrdine";
                    var cmd = GetCommand(query);
                    cmd.Parameters.Add(new SqlParameter("@IdOrdine", idOrdine));
                    using var conn = GetConnection();
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();

                    if (result != 1)
                    throw new Exception("Eliminazione non completata");
            }
        }
    }
}
