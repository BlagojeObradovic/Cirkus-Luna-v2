// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Seats
{
    public class IndexModel : PageModel
    {
        private readonly IPerformanceRepository _repo;
        public IndexModel(IPerformanceRepository repo)
        {
            _repo = repo;
        }

        public Performance? Performance { get; set; }
        public List<Performance> AllPerformances { get; set; } = new();
        public int? SelectedPerformanceId { get; set; }

        public void OnGet(int? performanceId)
        {
            SelectedPerformanceId = performanceId;
            AllPerformances = _repo.GetAll().ToList();

            if (performanceId.HasValue)
                Performance = _repo.GetById(performanceId.Value);
        }
    }
}
