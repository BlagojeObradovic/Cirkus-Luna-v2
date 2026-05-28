// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.TicketTypes
{
    public class CreateModel : PageModel
    {
        private readonly ITicketTypeRepository _repo;
        public CreateModel(ITicketTypeRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public TicketInput Ticket { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _repo.Add(new TicketType { Category = Ticket.Category, Name = Ticket.Name, Price = Ticket.Price, Description = Ticket.Description });
            TempData["Success"] = "Billettype oprettet!";
            return RedirectToPage("Index");
        }

        public class TicketInput
        {
            public TicketCategory Category { get; set; }
            [Required] public string Name { get; set; } = string.Empty;
            [Required, Range(0, 10000)] public decimal Price { get; set; }
            public string Description { get; set; } = string.Empty;
        }
    }
}
