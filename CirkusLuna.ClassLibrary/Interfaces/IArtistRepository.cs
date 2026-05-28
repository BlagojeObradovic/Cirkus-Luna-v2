// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface IArtistRepository
    {
        List<Artist> GetAll();
        Artist? GetById(int id);
        void Add(Artist artist);
        void Update(Artist artist);
        void Delete(int id);
        List<Artist> Search(string query);
        List<Artist> GetByType(ArtistType artistType);
        List<Artist> GetPermanent();
        List<Artist> GetByPerformance(int performanceId);
    }
}
