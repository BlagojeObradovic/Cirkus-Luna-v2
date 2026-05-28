// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository _repo;
        public DeleteModel(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        public string EmployeeName { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();
            EmployeeName = e.FullName;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Medarbejder slettet.";
            return RedirectToPage("Index");
        }
    }
}
