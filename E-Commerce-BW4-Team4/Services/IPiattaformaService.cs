using E_Commerce_BW4_Team4.Models;

namespace E_Commerce_BW4_Team4.Services
{
    public interface IPiattaformaService
    {
        IEnumerable<Piattaforma> GetAllPiattaforme();

        void Create(Piattaforma piattaforma);

        void Update(int piattafomaId);

        void Delete(int piattafomaId);
    }
}
