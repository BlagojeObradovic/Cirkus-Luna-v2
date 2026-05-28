// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly INewsRepository _repo;
        public IndexModel(INewsRepository repo)
        {
            _repo = repo;
        }

        public List<NewsPost> Posts { get; set; } = new();
        public string? Query { get; set; }

        public void OnGet(string? query)
        {
            Query = query;
            var all = string.IsNullOrWhiteSpace(query) ? _repo.GetAll() : _repo.Search(query);

            var sorted = all.ToList();
            for (int i = 1; i < sorted.Count; i++)
            {
                var key = sorted[i];
                int j = i - 1;
                while (j >= 0 && sorted[j].PublishedDate < key.PublishedDate)
                {
                    sorted[j + 1] = sorted[j];
                    j--;
                }
                sorted[j + 1] = key;
            }

            Posts = sorted;
        }
    }
}
