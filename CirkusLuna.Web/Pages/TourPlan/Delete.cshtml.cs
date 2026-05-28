// Ansvarlig: Altay
using CirkusLuna.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.TourPlan
{
    public class DeleteModel : PageModel
    {
        private readonly ICircusCityRepository _repo;
        public DeleteModel(ICircusCityRepository repo)
        {
            _repo = repo;
        }
        public string CityName { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var c = _repo.GetById(id);
            if (c == null) return NotFound();
            CityName = c.Name;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "By fjernet fra turnéplanen.";
            return RedirectToPage("Index");
        }
    }
}
