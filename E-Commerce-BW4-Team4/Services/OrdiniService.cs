using E_Commerce_BW4_Team4.Models;
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
                CoverImagePath = $"/Images/{reader.GetInt32(5)}a.jpg"
            };
        }
        public IEnumerable<OrdineCompleto> GetAllOrdine()
        {
            var query = "SELECT p.NomeProdotto, p.Brand, pt.NomePiattaforma, SUM(Quantita) as QuantitaTotale, SUM(p.Prezzo) as PrezzoTotale, p.IdProdotto FROM Ordini as o JOIN Carrello as C ON o.IdCarrello = c.IdCarrello JOIN Prodotti as p ON o.IdProdotti = p.IdProdotto JOIN Generi as g ON p.IdGenere = g.IdGenere JOIN Piattaforme as pt ON p.IdPiattaforma = pt.IdPiattaforma WHERE c.IdCarrello = 1 GROUP BY p.NomeProdotto, p.Brand, pt.NomePiattaforma, p.IdProdotto";
            var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var ListaOrdini = new List<OrdineCompleto>();
            while (reader.Read())
                ListaOrdini.Add(Create(reader));
            return ListaOrdini;
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

        public void Delete(int idProdotto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

    }
}
