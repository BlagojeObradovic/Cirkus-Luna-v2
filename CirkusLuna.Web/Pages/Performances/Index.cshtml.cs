// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Performances
{
    public class IndexModel : PageModel
    {
        private readonly IPerformanceRepository _repo;
        public IndexModel(IPerformanceRepository repo)
        {
            _repo = repo;
        }

        public List<Performance> Performances { get; set; } = new();
        public string? CityFilter { get; set; }
        public DateTime? DateFilter { get; set; }
        public int? TicketCategoryFilter { get; set; }
        public bool? HasSeatsFilter { get; set; }
        public bool IsFiltered { get; set; }

        public void OnGet(string? city, DateTime? date, int? ticketCategory, bool? hasSeats)
        {
            CityFilter = city;
            DateFilter = date;
            TicketCategoryFilter = ticketCategory;
            HasSeatsFilter = hasSeats;

            IsFiltered = !string.IsNullOrEmpty(city) || date.HasValue || ticketCategory.HasValue || hasSeats.HasValue;

            TicketCategory? category = ticketCategory.HasValue ? (TicketCategory)ticketCategory.Value : null;

            var result = _repo.Search(city, date, category, hasSeats);

            var sorted = result.ToList();
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

            Performances = sorted;
        }
    }
}
