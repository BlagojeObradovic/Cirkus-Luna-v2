// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.TicketTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ITicketTypeRepository _repo;
        public DeleteModel(ITicketTypeRepository repo)
        {
            _repo = repo;
        }
        public string TicketName { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var t = _repo.GetById(id);
            if (t == null) return NotFound();
            TicketName = t.Name;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Billettype slettet.";
            return RedirectToPage("Index");
        }
    }
}
