// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly List<NewsPost> _posts = new List<NewsPost>();
        private int _nextId = 1;

        public NewsRepository()
        {
            Add(new NewsPost { Title = "Cirkus Luna er klar til sommersæson 2026!", Content = "Vi er utroligt glade for at kunne annoncere, at Cirkus Luna er klar til at tage på turné igen! I år bringer vi et helt nyt program med nye artister og numre. Forbered dig på magi, akrobatik og masser af grin. Vi glæder os til at se jer under teltet!\n\nSæsonen starter den 1. maj og løber frem til den 31. august. Vi besøger over 20 byer i hele Danmark, fra nord til syd. Billetter er nu til salg – sikr dig din plads i god tid!", Author = "Jens Larsen", PublishedDate = new DateTime(2026, 4, 15), IsPublished = true });
            Add(new NewsPost { Title = "Ny artist: Carlos Ramirez – ildkunstner fra Spanien", Content = "Vi er stolte af at byde Carlos Ramirez velkommen til Cirkus Luna! Carlos er uddannet ildkunstner fra Sevilla i Spanien og har optrådt på scener over hele verden. Hans show er en eksplosiv blanding af ild og flamenco-musik. Han optræder ved udvalgte forestillinger – hold øje med programmet for at se hvornår han gæster din by!", Author = "Marie Hansen", PublishedDate = new DateTime(2026, 5, 1), IsPublished = true });
            Add(new NewsPost { Title = "VIP-oplevelsen – nu med champagne og meet & greet", Content = "Vores VIP-program er blevet opgraderet! Med en VIP-billet får du nu adgang til en eksklusiv velkomstdrink (champagne for voksne, juice for børn), de bedste sæder i huset, og mulighed for at møde artisterne efter forestillingen. VIP-pladserne er begrænsede til 10 pr. forestilling, så bestil din billet i dag!", Author = "Jens Larsen", PublishedDate = new DateTime(2026, 5, 10), IsPublished = true });
            Add(new NewsPost { Title = "Familiedage: Halv pris for børn under 5 år", Content = "I weekenderne tilbyder vi særlige familiedage, hvor børn under 5 år kommer gratis ind med en betalende voksen. Det er den perfekte mulighed for at introducere de yngste til cirkusverdenen. Tjek turnéplanen for at finde din nærmeste familiedag!", Author = "Marie Hansen", PublishedDate = new DateTime(2026, 5, 20), IsPublished = true });
        }

        public List<NewsPost> GetAll()
        {
            return _posts;
        }

        public NewsPost? GetById(int id)
        {
            for (int i = 0; i < _posts.Count; i++)
            {
                if (_posts[i].Id == id)
                    return _posts[i];
            }
            return null;
        }

        public void Add(NewsPost post)
        {
            post.Id = _nextId++;
            _posts.Add(post);
        }

        public void Update(NewsPost post)
        {
            for (int i = 0; i < _posts.Count; i++)
            {
                if (_posts[i].Id == post.Id)
                {
                    _posts[i] = post;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _posts.Count; i++)
            {
                if (_posts[i].Id == id)
                {
                    _posts.RemoveAt(i);
                    return;
                }
            }
        }

        public List<NewsPost> GetPublished()
        {
            var results = new List<NewsPost>();

            for (int i = 0; i < _posts.Count; i++)
            {
                if (_posts[i].IsPublished)
                    results.Add(_posts[i]);
            }

            return results;
        }

        public List<NewsPost> Search(string query)
        {
            var results = new List<NewsPost>();
            string lowerQuery = query.ToLower();

            for (int i = 0; i < _posts.Count; i++)
            {
                var p = _posts[i];
                if (p.Title.ToLower().Contains(lowerQuery) ||
                    p.Content.ToLower().Contains(lowerQuery) ||
                    p.Author.ToLower().Contains(lowerQuery))
                {
                    results.Add(p);
                }
            }

            return results;
        }
    }
}
