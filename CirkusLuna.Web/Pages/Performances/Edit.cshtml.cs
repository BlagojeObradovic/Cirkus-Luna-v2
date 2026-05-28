// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Performances
{
    public class EditModel : PageModel
    {
        private readonly IPerformanceRepository _perfRepo;
        private readonly ICircusCityRepository _cityRepo;
        private readonly IArtistRepository _artistRepo;

        public EditModel(IPerformanceRepository perfRepo, ICircusCityRepository cityRepo, IArtistRepository artistRepo)
        {
            _perfRepo = perfRepo;
            _cityRepo = cityRepo;
            _artistRepo = artistRepo;
        }

        [BindProperty]
        public PerformanceEditInput Performance { get; set; } = new();

        [BindProperty]
        public List<int> SelectedArtistIds { get; set; } = new();

        public List<CircusCity> Cities { get; set; } = new();
        public List<Artist> Artists { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var p = _perfRepo.GetById(id);
            if (p == null) return NotFound();

            Cities = _cityRepo.GetAll().ToList();
            Artists = _artistRepo.GetAll().ToList();

            Performance = new PerformanceEditInput
            {
                Id = p.Id,
                CityId = p.CityId,
                Date = p.Date,
                StartTime = p.StartTime,
                Description = p.Description
            };

            for (int i = 0; i < p.Artists.Count; i++)
                SelectedArtistIds.Add(p.Artists[i].Id);

            return Page();
        }

        public IActionResult OnPost()
        {
            Cities = _cityRepo.GetAll().ToList();
            Artists = _artistRepo.GetAll().ToList();

            if (!ModelState.IsValid) return Page();

            var existing = _perfRepo.GetById(Performance.Id);
            if (existing == null) return NotFound();

            existing.CityId = Performance.CityId;
            existing.City = _cityRepo.GetById(Performance.CityId);
            existing.Date = Performance.Date;
            existing.StartTime = Performance.StartTime;
            existing.Description = Performance.Description;

            existing.Artists.Clear();
            for (int i = 0; i < SelectedArtistIds.Count; i++)
            {
                var artist = _artistRepo.GetById(SelectedArtistIds[i]);
                if (artist != null)
                    existing.Artists.Add(artist);
            }

            _perfRepo.Update(existing);
            TempData["Success"] = "Forestilling opdateret!";
            return RedirectToPage("Index");
        }

        public class PerformanceEditInput
        {
            public int Id { get; set; }
            public int CityId { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan StartTime { get; set; }
            public string Description { get; set; } = string.Empty;
        }
    }
}
