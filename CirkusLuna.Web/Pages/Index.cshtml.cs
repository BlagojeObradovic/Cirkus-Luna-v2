using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICircusCityRepository _cityRepo;
        private readonly IPerformanceRepository _performanceRepo;
        private readonly IArtistRepository _artistRepo;
        private readonly INewsRepository _newsRepo;

        public IndexModel(
            ICircusCityRepository cityRepo,
            IPerformanceRepository performanceRepo,
            IArtistRepository artistRepo,
            INewsRepository newsRepo)
        {
            _cityRepo = cityRepo;
            _performanceRepo = performanceRepo;
            _artistRepo = artistRepo;
            _newsRepo = newsRepo;
        }

        public int TotalCities { get; set; }
        public int TotalPerformances { get; set; }
        public int TotalArtists { get; set; }
        public int UpcomingPerformances { get; set; }
        public List<Performance> NextPerformances { get; set; } = new();
        public List<NewsPost> LatestNews { get; set; } = new();

        public void OnGet()
        {
            TotalCities = _cityRepo.GetAll().Count();
            TotalPerformances = _performanceRepo.GetAll().Count();
            TotalArtists = _artistRepo.GetAll().Count();

            var future = _performanceRepo.GetFuture().ToList();
            UpcomingPerformances = future.Count;

            var sorted = new List<Performance>(future);
            for (int i = 1; i < sorted.Count; i++)
            {
                var key = sorted[i];
                int j = i - 1;
                while (j >= 0 && sorted[j].Date > key.Date)
                {
                    sorted[j + 1] = sorted[j];
                    j--;
                }
                sorted[j + 1] = key;
            }

            for (int i = 0; i < sorted.Count && i < 6; i++)
                NextPerformances.Add(sorted[i]);

            var allNews = _newsRepo.GetPublished().ToList();
            var sortedNews = new List<NewsPost>(allNews);
            for (int i = 1; i < sortedNews.Count; i++)
            {
                var key = sortedNews[i];
                int j = i - 1;
                while (j >= 0 && sortedNews[j].PublishedDate < key.PublishedDate)
                {
                    sortedNews[j + 1] = sortedNews[j];
                    j--;
                }
                sortedNews[j + 1] = key;
            }

            for (int i = 0; i < sortedNews.Count && i < 4; i++)
                LatestNews.Add(sortedNews[i]);
        }
    }
}
