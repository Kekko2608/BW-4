﻿using E_Commerce_BW4_Team4.Models;
using System.Data.SqlClient;
using System.Data.Common;

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
        public ProdottoCompleto  Create(DbDataReader reader)
        {
            return new ProdottoCompleto
            {
                NomeProdotto = reader.GetString(0),
                DescrizioneProdotto = reader.GetString(1),
                Brand = reader.GetString(2),
                PEGI = reader.GetString(3),
                Disponibilita = reader.GetBoolean(4),
                Prezzo = reader.GetDecimal(5),
                TipoDiGenere = reader.GetString(6),
                NomePiattaforma = reader.GetString(7)
            };
        }
        public IEnumerable<ProdottoCompleto> GetAllProducts()
        {
            var query = "SELECT p.NomeProdotto, p.DescrizioneProdotto, p.Brand, p.PEGI, p.Disponibilita, p.Prezzo, g.TipoDiGenere, pt.NomePiattaforma FROM Prodotti as p JOIN Generi as g ON p.IdGenere = g.IdGenere " +
            "JOIN Piattaforme as pt ON p.IdPiattaforma = pt.IdPiattaforma";

            var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var ListaProdotti = new List<ProdottoCompleto>();
            while (reader.Read())
                ListaProdotti.Add(Create(reader));
            return ListaProdotti;
            
        }

        // ID PRODOTTO
        public Prodotto GetById(int IdProdotto)
        {
            var query = "SELECT IdProdotto FROM Prodotti WHERE IdProdotto = @IdProdotto";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@IdProdotto", IdProdotto));

            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var result = cmd.ExecuteNonQuery();
            if (reader.Read())
            {
                Prodotto prodotto = new Prodotto()
                {
                    IdProdotto = (int)reader["IdProdotto"],
                };
                return prodotto;
            }
            else
            {
                throw new Exception("Prodotto non trovato.");
            }
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
            prodotto.IdProdotto = Convert.ToInt32(result);
        }

        // DELETE PRODOTTO
        public void Delete(int IdProdotto)
        {
            var query = "DELETE FROM Prodotti WHERE IdProdotto = @IdProdotto";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@IdProdotto", IdProdotto));

            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteNonQuery();
            if (result != 1) throw new Exception("Articolo non eliminato");
        }
        // CONFERMA DELETE
        

        //UPDATE
        public void Update(Prodotto prodotto)
        {
            throw new NotImplementedException();
        }

        public void SaveImages(int idProdotto, IFormFile ImageA, IFormFile ImageB, IFormFile ImageC, IFormFile ImageD)
        {
            var images = new List<IFormFile> { ImageA, ImageB, ImageC, ImageD };
            var imageNames = new[] { "a", "b", "c", "d" };

            for (int i = 0; i < images.Count; i++)
            {
                if (images[i] != null && images[i].Length > 0)
                {
                    var imagePath = Path.Combine("wwwroot", "Images", $"{idProdotto}{imageNames[i]}.jpg");

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        images[i].CopyTo(stream);
                    }
                }
            }
        }
    }
}
