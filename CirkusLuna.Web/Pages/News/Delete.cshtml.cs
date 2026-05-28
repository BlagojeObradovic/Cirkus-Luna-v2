// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.News
{
    public class DeleteModel : PageModel
    {
        private readonly INewsRepository _repo;
        public DeleteModel(INewsRepository repo)
        {
            _repo = repo;
        }
        public string PostTitle { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            PostTitle = p.Title;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Indlæg slettet.";
            return RedirectToPage("Index");
        }
    }
}
