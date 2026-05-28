// Ansvarlig: Altay
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.TourPlan
{
    public class IndexModel : PageModel
    {
        private readonly ICircusCityRepository _repo;
        public IndexModel(ICircusCityRepository repo)
        {
            _repo = repo;
        }

        public List<CircusCity> Cities { get; set; } = new();
        public string? Query { get; set; }

        public void OnGet(string? query)
        {
            Query = query;

            IEnumerable<CircusCity> result;

            if (string.IsNullOrWhiteSpace(query))
            {
                result = _repo.GetSortedAlphabetically();
            }
            else
            {
                result = _repo.Search(query);
            }

            Cities = result.ToList();
        }
    }
}
