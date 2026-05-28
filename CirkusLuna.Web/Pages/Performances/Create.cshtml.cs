// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Performances
{
    public class CreateModel : PageModel
    {
        private readonly IPerformanceRepository _perfRepo;
        private readonly ICircusCityRepository _cityRepo;
        private readonly IArtistRepository _artistRepo;

        public CreateModel(IPerformanceRepository perfRepo, ICircusCityRepository cityRepo, IArtistRepository artistRepo)
        {
            _perfRepo = perfRepo;
            _cityRepo = cityRepo;
            _artistRepo = artistRepo;
        }

        [BindProperty]
        public PerformanceInput Performance { get; set; } = new();

        [BindProperty]
        public List<int> SelectedArtistIds { get; set; } = new();

        public List<CircusCity> Cities { get; set; } = new();
        public List<Artist> Artists { get; set; } = new();

        public void OnGet()
        {
            Cities = _cityRepo.GetAll().ToList();
            Artists = _artistRepo.GetAll().ToList();
        }

        public IActionResult OnPost()
        {
            Cities = _cityRepo.GetAll().ToList();
            Artists = _artistRepo.GetAll().ToList();

            if (!ModelState.IsValid) return Page();

            var city = _cityRepo.GetById(Performance.CityId);
            var performance = new Performance
            {
                CityId = Performance.CityId,
                City = city,
                Date = Performance.Date,
                StartTime = Performance.StartTime,
                Description = Performance.Description,
                IsActive = true
            };

            for (int i = 0; i < SelectedArtistIds.Count; i++)
            {
                var artist = _artistRepo.GetById(SelectedArtistIds[i]);
                if (artist != null)
                {
                    performance.Artists.Add(artist);
                    artist.Performances.Add(performance);
                }
            }

            _perfRepo.Add(performance);

            if (city != null)
                city.Performances.Add(performance);

            TempData["Success"] = "Forestilling oprettet!";
            return RedirectToPage("Index");
        }

        public class PerformanceInput
        {
            [Required(ErrorMessage = "Vælg en by")]
            [Range(1, int.MaxValue, ErrorMessage = "Vælg en by")]
            public int CityId { get; set; }

            [Required(ErrorMessage = "Dato er påkrævet")]
            public DateTime Date { get; set; } = DateTime.Today.AddDays(1);

            public TimeSpan StartTime { get; set; } = new TimeSpan(19, 0, 0);
            public string Description { get; set; } = string.Empty;
        }
    }
}
