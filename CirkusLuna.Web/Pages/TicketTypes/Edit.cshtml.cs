// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.TicketTypes
{
    public class EditModel : PageModel
    {
        private readonly ITicketTypeRepository _repo;
        public EditModel(ITicketTypeRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public TicketEditInput Ticket { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var t = _repo.GetById(id);
            if (t == null) return NotFound();
            Ticket = new TicketEditInput { Id = t.Id, Category = t.Category, Name = t.Name, Price = t.Price, Description = t.Description };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            var existing = _repo.GetById(Ticket.Id);
            if (existing == null) return NotFound();
            existing.Category = Ticket.Category;
            existing.Name = Ticket.Name;
            existing.Price = Ticket.Price;
            existing.Description = Ticket.Description;
            _repo.Update(existing);
            TempData["Success"] = "Billettype opdateret!";
            return RedirectToPage("Index");
        }

        public class TicketEditInput
        {
            public int Id { get; set; }
            public TicketCategory Category { get; set; }
            [Required] public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string Description { get; set; } = string.Empty;
        }
    }
}
