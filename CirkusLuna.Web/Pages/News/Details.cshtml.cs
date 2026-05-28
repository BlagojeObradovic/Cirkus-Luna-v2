// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly INewsRepository _repo;
        public DetailsModel(INewsRepository repo)
        {
            _repo = repo;
        }

        public NewsPost? Post { get; set; }

        public void OnGet(int id)
        {
            Post = _repo.GetById(id);
        }
    }
}
