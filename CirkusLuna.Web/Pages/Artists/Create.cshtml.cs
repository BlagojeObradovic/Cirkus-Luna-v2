// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Artists
{
    public class CreateModel : PageModel
    {
        private readonly IArtistRepository _repo;
        public CreateModel(IArtistRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public ArtistInput Artist { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _repo.Add(new Artist
            {
                FirstName = Artist.FirstName,
                LastName = Artist.LastName,
                Email = Artist.Email,
                Phone = Artist.Phone,
                ArtistType = Artist.ArtistType,
                Nationality = Artist.Nationality,
                ActDescription = Artist.ActDescription,
                IsPermanent = Artist.IsPermanent
            });

            TempData["Success"] = "Artist tilføjet!";
            return RedirectToPage("Index");
        }

        public class ArtistInput
        {
            [Required(ErrorMessage = "Fornavn er påkrævet")]
            public string FirstName { get; set; } = string.Empty;
            [Required(ErrorMessage = "Efternavn er påkrævet")]
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public ArtistType ArtistType { get; set; }
            public string Nationality { get; set; } = string.Empty;
            public string ActDescription { get; set; } = string.Empty;
            public bool IsPermanent { get; set; } = true;
        }
    }
}
