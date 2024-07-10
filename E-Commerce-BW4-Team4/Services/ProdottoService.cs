using E_Commerce_BW4_Team4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace E_Commerce_BW4_Team4.Services
{
    public class ProdottoService : SqlServerServiceBase, IProdottoService
    {
        public ProdottoService(IConfiguration config) : base(config) { }

        // RECUPERA TUTTI I PRODOTTI
        public IEnumerable<Prodotto> GetAllProducts()
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
                NomePiattaforma = reader.GetString(7),
                IdProdotto = reader.GetInt32(8)
            };
        }

        public IEnumerable<ProdottoViewModel> GetAllProductsWithImages()
        {
            var prodotti = GetAllProducts();
            var prodottiConImmagini = new List<ProdottoViewModel>();

            foreach (var prodotto in prodotti)
            {
                var coverImagePath = Path.Combine("wwwroot", "Images", $"{prodotto.IdProdotto}a.jpg");
                var prodottoViewModel = new ProdottoViewModel
                {
                    IdProdotto = prodotto.IdProdotto,
                    NomeProdotto = prodotto.NomeProdotto,
                    DescrizioneProdotto = prodotto.DescrizioneProdotto,
                    Disponibilita = prodotto.Disponibilita,
                    Prezzo = prodotto.Prezzo,
                    TipoDiGenere = prodotto.TipoDiGenere,
                    NomePiattaforma = prodotto.NomePiattaforma,
                    CoverImagePath = File.Exists(coverImagePath) ? $"/Images/{prodotto.IdProdotto}a.jpg" : "/Images/default.jpg"
                };
                prodottiConImmagini.Add(prodottoViewModel);
            }

            return prodottiConImmagini;
        }

        public IEnumerable<ProdottoCompleto> GetAllProducts()
        {
            var query = "SELECT p.NomeProdotto, p.DescrizioneProdotto, p.Brand, p.PEGI, p.Disponibilita, p.Prezzo, g.TipoDiGenere, pt.NomePiattaforma, p.IdProdotto FROM Prodotti as p JOIN Generi as g ON p.IdGenere = g.IdGenere " +
            "JOIN Piattaforme as pt ON p.IdPiattaforma = pt.IdPiattaforma";

            using (var conn = (SqlConnection)GetConnection())
            {
                var query = "SELECT * FROM Prodotti";
                var cmd = new SqlCommand(query, conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prodotto = new Prodotto
                        {
                            IdProdotto = reader.GetInt32(0),
                            NomeProdotto = reader.GetString(1),
                            DescrizioneProdotto = reader.GetString(2),
                            Brand = reader.GetString(3),
                            PEGI = reader.GetString(4),
                            CodiceABarre = reader.GetString(5),
                            Disponibilita = reader.GetBoolean(6),
                            Prezzo = reader.GetDecimal(7),
                            Piattaforma = reader.GetInt32(8),
                            Genere = reader.GetInt32(9)
                        };
                        prodotti.Add(prodotto);
                    }
                }
            }

            return prodotti;
        }

        // RECUPERA TUTTI I PRODOTTI CON IMMAGINI
        public IEnumerable<ProdottoViewModel> GetAllProductsWithImages()
        {
            var prodotti = GetAllProducts();
            var prodottiConImmagini = new List<ProdottoViewModel>();

            foreach (var prodotto in prodotti)
            {
                var coverImagePath = Path.Combine("wwwroot", "Images", $"{prodotto.IdProdotto}a.jpg");
                var prodottoViewModel = new ProdottoViewModel
                {
                    IdProdotto = prodotto.IdProdotto,
                    NomeProdotto = prodotto.NomeProdotto,
                    DescrizioneProdotto = prodotto.DescrizioneProdotto,
                    Disponibilita = prodotto.Disponibilita,
                    Prezzo = prodotto.Prezzo,
                    CoverImagePath = File.Exists(coverImagePath) ? $"/Images/{prodotto.IdProdotto}a.jpg" : "/Images/default.jpg"
                };
                prodottiConImmagini.Add(prodottoViewModel);
            }

            return prodottiConImmagini;
        }

        // ID PRODOTTO
        public Prodotto GetById(int IdProdotto)
        {
            var query = "SELECT IdProdotto, NomeProdotto, DescrizioneProdotto, Brand, PEGI, CodiceAbarre, Disponibilita, Prezzo, IdPiattaforma, IdGenere FROM Prodotti WHERE IdProdotto = @IdProdotto";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@IdProdotto", IdProdotto));

            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Prodotto prodotto = new Prodotto()
                {
                    IdProdotto = (int)reader["IdProdotto"],
                    NomeProdotto = reader.GetString(1),
                    DescrizioneProdotto = reader.GetString(2),
                    Brand = reader.GetString(3),
                    PEGI = reader.GetString(4),
                    CodiceABarre = reader.GetString(5),
                    Disponibilita = reader.GetBoolean(6),
                    Prezzo = reader.GetDecimal(7),
                    Piattaforma = (int)reader["IdPiattaforma"],
                    Genere = (int)reader["IdGenere"],


                };
                return prodotto;
            }
            else
            {
                throw new Exception("Prodotto non trovato.");
            }

            return null;
        }

        // CREATE ARTICOLO
        public void Create(Prodotto prodotto)
        {
            var query = "INSERT INTO Prodotti (NomeProdotto, DescrizioneProdotto, Brand, PEGI, CodiceABarre, Disponibilita, Prezzo, IdPiattaforma, IdGenere) " +
                        "VALUES (@NomeProdotto, @DescrizioneProdotto, @Brand, @PEGI, @CodiceABarre, @Disponibilita, @Prezzo, @IdPiattaforma, @IdGenere); " +
                        "SELECT SCOPE_IDENTITY();";
            using var conn = (SqlConnection)GetConnection();
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@NomeProdotto", prodotto.NomeProdotto));
            cmd.Parameters.Add(new SqlParameter("@DescrizioneProdotto", prodotto.DescrizioneProdotto));
            cmd.Parameters.Add(new SqlParameter("@Brand", prodotto.Brand));
            cmd.Parameters.Add(new SqlParameter("@PEGI", prodotto.PEGI));
            cmd.Parameters.Add(new SqlParameter("@CodiceABarre", prodotto.CodiceABarre));
            cmd.Parameters.Add(new SqlParameter("@Disponibilita", prodotto.Disponibilita));
            cmd.Parameters.Add(new SqlParameter("@Prezzo", prodotto.Prezzo));
            cmd.Parameters.Add(new SqlParameter("@IdPiattaforma", prodotto.Piattaforma));
            cmd.Parameters.Add(new SqlParameter("@IdGenere", prodotto.Genere));

            conn.Open();
            var result = cmd.ExecuteScalar();

            if (result == null)
                throw new Exception("Creazione non completata");

            prodotto.IdProdotto = Convert.ToInt32(result);
        }

        // DELETE PRODOTTO
        public void Delete(int IdProdotto)
        {
            var query = "DELETE FROM Prodotti WHERE IdProdotto = @IdProdotto";
            using var conn = (SqlConnection)GetConnection();
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@IdProdotto", IdProdotto);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(Prodotto prodotto)
        {
            var query = "UPDATE Prodotti SET NomeProdotto = @NomeProdotto, DescrizioneProdotto = @DescrizioneProdotto, Brand = @Brand, PEGI = @PEGI, " +
                        "CodiceABarre = @CodiceABarre, Disponibilita = @Disponibilita, Prezzo = @Prezzo, IdPiattaforma = @IdPiattaforma, IdGenere = @IdGenere " +
                        "WHERE IdProdotto = @IdProdotto";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@IdProdotto", prodotto.IdProdotto));
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

        public void SaveImages(int idProdotto, IFormFile ImageA, IFormFile ImageB, IFormFile ImageC, IFormFile ImageD)
        {
            var images = new List<IFormFile> { ImageA, ImageB, ImageC, ImageD };
            var imageNames = new[] { "a", "b", "c", "d" };
            var imageDirectory = Path.Combine("wwwroot", "Images");

            // Controlla se la directory esiste, se no, la crea
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            for (int i = 0; i < images.Count; i++)
            {
                if (images[i] != null && images[i].Length > 0)
                {
                    var imagePath = Path.Combine(imageDirectory, $"{idProdotto}{imageNames[i]}.jpg");

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        images[i].CopyTo(stream);
                    }
                }
            }
        }

        
    }
}
