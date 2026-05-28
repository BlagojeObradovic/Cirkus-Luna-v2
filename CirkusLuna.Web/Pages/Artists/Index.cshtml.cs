// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Artists
{
    public class IndexModel : PageModel
    {
        private readonly IArtistRepository _repo;
        public IndexModel(IArtistRepository repo)
        {
            _repo = repo;
        }

        public List<Artist> Artists { get; set; } = new();
        public string? Query { get; set; }
        public int? TypeFilter { get; set; }

        public void OnGet(string? query, int? typeFilter)
        {
            Query = query;
            TypeFilter = typeFilter;

            var all = string.IsNullOrWhiteSpace(query)
                ? _repo.GetAll()
                : _repo.Search(query);

            if (typeFilter.HasValue)
            {
                var type = (ArtistType)typeFilter.Value;
                var filtered = new List<Artist>();
                foreach (var a in all)
                {
                    if (a.ArtistType == type)
                        filtered.Add(a);
                }
                Artists = filtered;
            }
            else
            {
                Artists = all.ToList();
            }
        }
    }
}
