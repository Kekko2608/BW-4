using E_Commerce_BW4_Team4.Models;
using System.Data.SqlClient;

namespace E_Commerce_BW4_Team4.Services
{
    public class ProdottoService : SqlServerServiceBase, IProdottoService
    {
        // LISTA PRODOTTI 
        public static List<Prodotto> _prodotto = new List<Prodotto>();

        public ProdottoService(IConfiguration config) : base(config)
        {
        }

        // RECUPERA TUTTI I PRODOTTI
        public IEnumerable<Prodotto> GetAllProducts()
        {
            return _prodotto;
        }

        // ID PRODOTTO
        public Prodotto GetById(int IdProdotto)
        {
            return _prodotto.FirstOrDefault(p => p.IdProdotto == IdProdotto);
        }

        // CREATE ARTICOLO
        public void Create(Prodotto prodotto)
        {
            var query = "INSERT INTO Prodotti (NomeProdotto, DescrizioneProdotto, Brand, PEGI, CodiceABarre, Disponibilita, Prezzo, IdPiattaforma, IdGenere) " +
                        "VALUES (@NomeProdotto, @DescrizioneProdotto, @Brand, @PEGI, @CodiceABarre, @Disponibilita, @Prezzo, @IdPiattaforma, @IdGenere)";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@NomeProdotto", prodotto.NomeProdotto));
            cmd.Parameters.Add(new SqlParameter("@DescrizioneProdotto", prodotto.DescrizioneProdotto));
            cmd.Parameters.Add(new SqlParameter("@Brand", prodotto.Brand));
            cmd.Parameters.Add(new SqlParameter("@PEGI", prodotto.PEGI));
            cmd.Parameters.Add(new SqlParameter("@CodiceABarre", prodotto.CodiceABarre));
            cmd.Parameters.Add(new SqlParameter("@Disponibilita", prodotto.Disponibilita));
            cmd.Parameters.Add(new SqlParameter("@Prezzo", prodotto.Prezzo));
            cmd.Parameters.Add(new SqlParameter("@IdPiattaforma", prodotto.Piattaforma));
            cmd.Parameters.Add(new SqlParameter("@IdGenere", prodotto.Genere));

            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteNonQuery();
   
            if (result != 1)
                throw new Exception("Creazione non completata");
        }

        // DELETE PRODOTTO
        public void Delete(int IdProdotto)
        {
            var prodotto = GetById(IdProdotto);
            if (prodotto != null)
            {
                _prodotto.Remove(prodotto);
            }
        }

        public void Update(Prodotto prodotto)
        {
            throw new NotImplementedException();
        }
    }
}
