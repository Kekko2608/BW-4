using E_Commerce_BW4_Team4.Models;

namespace E_Commerce_BW4_Team4.Services
{
    public class ProdottoService : IProdottoService
    {
        // LISTA PRODOTTI 
        public static List<Prodotto> _prodotto = new List<Prodotto>();

        // RECUPERA TUTTI I PRODOTTI
        public IEnumerable<Prodotto> GetAllProducts()
        {
            return _prodotto;
        }

        // ID PRODOTTO
        public Prodotto GetById(int IdProdotto)
        {
            return _prodotto.FirstOrDefault(p => p.IdProdotto == IdProdotto)
        }

        // CREATE ARTICOLO
        public void Create(Prodotto prodotto)
        {
            var query = "INSERT INTO Prodotti (NomeProdotto, DescrizioneProdotto, Brand, PEGI, CodiceABarre, Disponibilita, Prezzo, IdPiattaforma, IdGenere, IdImmagini) " +
                        "VALUES (@NomeProdotto, @DescrizioneProdotto, @Brand, @PEGI, @CodiceABarre, @Disponibilita, @Prezzo, @IdPiattaforma, @IdGenere, @IdImmagini)";
            var cmd = GetCommand(query);
            cmd.Parameters.AddWithValue("@NomeProdotto", prodotto.NomeProdotto);
            cmd.Parameters.AddWithValue("@DescrizioneProdotto", prodotto.DescrizioneProdotto);
            cmd.Parameters.AddWithValue("@Brand", prodotto.Brand);
            cmd.Parameters.AddWithValue("@PEGI", prodotto.PEGI);
            cmd.Parameters.AddWithValue("@CodiceABarre", prodotto.CodiceABarre);
            cmd.Parameters.AddWithValue("@Disponibilita", prodotto.Disponibilita);
            cmd.Parameters.AddWithValue("@Prezzo", prodotto.Prezzo);
            cmd.Parameters.AddWithValue("@IdPiattaforma", prodotto.Piattaforma);
            cmd.Parameters.AddWithValue("@IdGenere", prodotto.Genere);
            cmd.Parameters.AddWithValue("@IdImmagini", prodotto.Immagine);

            using var conn = GetConnection();
            cmd.Connection = conn;
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

        
    }
}
