// Ansvarlig: Altay
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.TourPlan
{
    public class CreateModel : PageModel
    {
        private readonly ICircusCityRepository _repo;
        public CreateModel(ICircusCityRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public CityInput City { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _repo.Add(new CircusCity { Name = City.Name, Venue = City.Venue, Address = City.Address });
            TempData["Success"] = "By tilføjet til turnéen!";
            return RedirectToPage("Index");
        }

        public class CityInput
        {
            [Required(ErrorMessage = "Bynavn er påkrævet")]
            public string Name { get; set; } = string.Empty;
            [Required(ErrorMessage = "Spillested er påkrævet")]
            public string Venue { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
        }
    }
}
