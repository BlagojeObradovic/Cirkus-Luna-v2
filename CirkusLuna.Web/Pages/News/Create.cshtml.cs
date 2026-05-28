// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly INewsRepository _repo;
        public CreateModel(INewsRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public NewsInput Post { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _repo.Add(new NewsPost
            {
                Title = Post.Title,
                Content = Post.Content,
                Author = Post.Author,
                PublishedDate = Post.PublishedDate,
                IsPublished = Post.IsPublished
            });

            TempData["Success"] = "Indlæg publiceret!";
            return RedirectToPage("Index");
        }

        public class NewsInput
        {
            [Required(ErrorMessage = "Titel er påkrævet")]
            public string Title { get; set; } = string.Empty;
            [Required(ErrorMessage = "Indhold er påkrævet")]
            public string Content { get; set; } = string.Empty;
            public string Author { get; set; } = string.Empty;
            public DateTime PublishedDate { get; set; } = DateTime.Today;
            public bool IsPublished { get; set; } = true;
        }
    }
}
