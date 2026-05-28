// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Artists
{
    public class EditModel : PageModel
    {
        private readonly IArtistRepository _repo;
        public EditModel(IArtistRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public ArtistEditInput Artist { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var a = _repo.GetById(id);
            if (a == null) return NotFound();

            Artist = new ArtistEditInput
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                Phone = a.Phone,
                ArtistType = a.ArtistType,
                Nationality = a.Nationality,
                ActDescription = a.ActDescription,
                IsPermanent = a.IsPermanent
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var existing = _repo.GetById(Artist.Id);
            if (existing == null) return NotFound();

            existing.FirstName = Artist.FirstName;
            existing.LastName = Artist.LastName;
            existing.Email = Artist.Email;
            existing.Phone = Artist.Phone;
            existing.ArtistType = Artist.ArtistType;
            existing.Nationality = Artist.Nationality;
            existing.ActDescription = Artist.ActDescription;
            existing.IsPermanent = Artist.IsPermanent;

            _repo.Update(existing);
            TempData["Success"] = "Artist opdateret!";
            return RedirectToPage("Index");
        }

        public class ArtistEditInput
        {
            public int Id { get; set; }
            [Required] public string FirstName { get; set; } = string.Empty;
            [Required] public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public ArtistType ArtistType { get; set; }
            public string Nationality { get; set; } = string.Empty;
            public string ActDescription { get; set; } = string.Empty;
            public bool IsPermanent { get; set; }
        }
    }
}
