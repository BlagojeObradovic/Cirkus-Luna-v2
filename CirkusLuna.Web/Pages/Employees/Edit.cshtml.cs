// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _repo;
        public EditModel(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public EmployeeEditInput Employee { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();
            Employee = new EmployeeEditInput { Id = e.Id, FirstName = e.FirstName, LastName = e.LastName, Email = e.Email, Phone = e.Phone, Role = e.Role, HireDate = e.HireDate, IsActive = e.IsActive };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            var existing = _repo.GetById(Employee.Id);
            if (existing == null) return NotFound();
            existing.FirstName = Employee.FirstName;
            existing.LastName = Employee.LastName;
            existing.Email = Employee.Email;
            existing.Phone = Employee.Phone;
            existing.Role = Employee.Role;
            existing.HireDate = Employee.HireDate;
            existing.IsActive = Employee.IsActive;
            _repo.Update(existing);
            TempData["Success"] = "Medarbejder opdateret!";
            return RedirectToPage("Index");
        }

        public class EmployeeEditInput
        {
            public int Id { get; set; }
            [Required] public string FirstName { get; set; } = string.Empty;
            [Required] public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public EmployeeRole Role { get; set; }
            public DateTime HireDate { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
