// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Performances
{
    public class DetailsModel : PageModel
    {
        private readonly IPerformanceRepository _repo;
        public DetailsModel(IPerformanceRepository repo)
        {
            _repo = repo;
        }

        public Performance? Performance { get; set; }

        public void OnGet(int id)
        {
            Performance = _repo.GetById(id);
        }
    }
}
