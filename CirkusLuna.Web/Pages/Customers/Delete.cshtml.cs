// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepository _repo;
        public DeleteModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public string CustomerName { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var c = _repo.GetById(id);
            if (c == null) return NotFound();
            CustomerName = c.FullName;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Kunde slettet.";
            return RedirectToPage("Index");
        }
    }
}
