// Ansvarlig: Altay
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface ICircusCityRepository
    {
        List<CircusCity> GetAll();
        CircusCity? GetById(int id);
        void Add(CircusCity city);
        void Update(CircusCity city);
        void Delete(int id);
        List<CircusCity> GetSortedAlphabetically();
        List<CircusCity> Search(string query);
    }
}
