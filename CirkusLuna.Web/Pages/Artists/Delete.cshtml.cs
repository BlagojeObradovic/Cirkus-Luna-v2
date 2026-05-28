// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Artists
{
    public class DeleteModel : PageModel
    {
        private readonly IArtistRepository _repo;
        public DeleteModel(IArtistRepository repo)
        {
            _repo = repo;
        }
        public string ArtistName { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var a = _repo.GetById(id);
            if (a == null) return NotFound();
            ArtistName = a.FullName;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Artist slettet.";
            return RedirectToPage("Index");
        }
    }
}
