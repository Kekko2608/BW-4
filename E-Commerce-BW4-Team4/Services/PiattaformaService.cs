using E_Commerce_BW4_Team4.Models;
using System.Data.Common;

namespace E_Commerce_BW4_Team4.Services
{
    public class PiattaformaService : SqlServerServiceBase, IPiattaformaService
    {

        public PiattaformaService(IConfiguration config) : base(config)
        {
        }

        public void Create(Piattaforma piattaforma)
        {
            throw new NotImplementedException();
        }

        public void Delete(int piattafomaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Piattaforma> GetAllPiattaforme()
        {
            var query = "SELECT IdPiattaforma, NomePiattaforma FROM Piattaforme";
            var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var ListaPiattaforme = new List<Piattaforma>();
            while (reader.Read())
                ListaPiattaforme.Add(Create(reader));
            return ListaPiattaforme;

        }

        public Piattaforma Create(DbDataReader reader)
        {
            return new Piattaforma
            {
                NomePiattaforma = reader.GetString(1),
                IdPiattaforma = reader.GetInt32(0),
            };

        }

        

        public void Update(int piattafomaId)
        {
            throw new NotImplementedException();
        }

    }
}
