// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly IReservationRepository _repo;
        public IndexModel(IReservationRepository repo)
        {
            _repo = repo;
        }

        public List<Reservation> Reservations { get; set; } = new();
        public string? Query { get; set; }

        public void OnGet(string? query)
        {
            Query = query;
            var all = string.IsNullOrWhiteSpace(query) ? _repo.GetAll() : _repo.Search(query);
            Reservations = all.ToList();
        }
    }
}
