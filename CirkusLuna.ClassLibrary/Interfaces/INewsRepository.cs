// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface INewsRepository
    {
        List<NewsPost> GetAll();
        NewsPost? GetById(int id);
        void Add(NewsPost post);
        void Update(NewsPost post);
        void Delete(int id);
        List<NewsPost> GetPublished();
        List<NewsPost> Search(string query);
    }
}
