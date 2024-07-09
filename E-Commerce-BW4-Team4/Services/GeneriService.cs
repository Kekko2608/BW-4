
using E_Commerce_BW4_Team4.Models;
using System.Data.Common;

namespace E_Commerce_BW4_Team4.Services
{
    public class GeneriService : SqlServerServiceBase, IGeneriService
    {
        public GeneriService(IConfiguration config) : base(config)
        {
        }

        public void Create(Genere genere)
        {
            throw new NotImplementedException();
        }

        public void Delete(int genereId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genere> GetAllGeneri()
        {
            var query = "SELECT TipoDiGenere FROM Generi";
            var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var ListaGeneri = new List<Genere>();
            while (reader.Read())
                ListaGeneri.Add(Create(reader));
            return ListaGeneri;
        }

        public Genere Create(DbDataReader reader)
        {
            return new Genere
            {
                TipoDiGenere = reader.GetString(0),
            };
        }
        public void Update(int genereId)
        {
            throw new NotImplementedException();
        }
    }
}
