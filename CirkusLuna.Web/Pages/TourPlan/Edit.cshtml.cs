// Ansvarlig: Altay
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.TourPlan
{
    public class EditModel : PageModel
    {
        private readonly ICircusCityRepository _repo;
        public EditModel(ICircusCityRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public CityEditInput City { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var c = _repo.GetById(id);
            if (c == null) return NotFound();
            City = new CityEditInput { Id = c.Id, Name = c.Name, Venue = c.Venue, Address = c.Address };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            var existing = _repo.GetById(City.Id);
            if (existing == null) return NotFound();
            existing.Name = City.Name;
            existing.Venue = City.Venue;
            existing.Address = City.Address;
            _repo.Update(existing);
            TempData["Success"] = "By opdateret!";
            return RedirectToPage("Index");
        }

        public class CityEditInput
        {
            public int Id { get; set; }
            [Required] public string Name { get; set; } = string.Empty;
            public string Venue { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
        }
    }
}
