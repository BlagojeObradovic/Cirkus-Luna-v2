// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Performances
{
    public class DeleteModel : PageModel
    {
        private readonly IPerformanceRepository _repo;
        public DeleteModel(IPerformanceRepository repo)
        {
            _repo = repo;
        }
        public string PerformanceName { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            PerformanceName = $"{p.City?.Name} – {p.DateDisplay}";
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Forestilling slettet.";
            return RedirectToPage("Index");
        }
    }
}
