// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.News
{
    public class EditModel : PageModel
    {
        private readonly INewsRepository _repo;
        public EditModel(INewsRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public NewsEditInput Post { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            Post = new NewsEditInput { Id = p.Id, Title = p.Title, Content = p.Content, Author = p.Author, PublishedDate = p.PublishedDate, IsPublished = p.IsPublished };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            var existing = _repo.GetById(Post.Id);
            if (existing == null) return NotFound();
            existing.Title = Post.Title;
            existing.Content = Post.Content;
            existing.Author = Post.Author;
            existing.PublishedDate = Post.PublishedDate;
            existing.IsPublished = Post.IsPublished;
            _repo.Update(existing);
            TempData["Success"] = "Indlæg opdateret!";
            return RedirectToPage("Index");
        }

        public class NewsEditInput
        {
            public int Id { get; set; }
            [Required] public string Title { get; set; } = string.Empty;
            [Required] public string Content { get; set; } = string.Empty;
            public string Author { get; set; } = string.Empty;
            public DateTime PublishedDate { get; set; }
            public bool IsPublished { get; set; }
        }
    }
}
