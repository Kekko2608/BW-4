using E_Commerce_BW4_Team4.Models;

namespace E_Commerce_BW4_Team4.Services
{
    public interface IGeneriService
    {
        IEnumerable<Genere> GetAllGeneri();

        void Create(Genere genere);

        void Update(int genereId);

        void Delete(int genereId);
    }
}
