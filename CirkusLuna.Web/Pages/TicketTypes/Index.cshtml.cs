// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.TicketTypes
{
    public class IndexModel : PageModel
    {
        private readonly ITicketTypeRepository _repo;
        public IndexModel(ITicketTypeRepository repo)
        {
            _repo = repo;
        }

        public List<TicketType> TicketTypes { get; set; } = new();

        public void OnGet()
        {
            TicketTypes = _repo.GetAll().ToList();
        }
    }
}
