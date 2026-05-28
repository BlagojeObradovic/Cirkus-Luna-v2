// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly IEmployeeRepository _repo;
        public CreateModel(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public EmployeeInput Employee { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _repo.Add(new Employee
            {
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                Email = Employee.Email,
                Phone = Employee.Phone,
                Role = Employee.Role,
                HireDate = Employee.HireDate,
                IsActive = Employee.IsActive
            });

            TempData["Success"] = "Medarbejder oprettet!";
            return RedirectToPage("Index");
        }

        public class EmployeeInput
        {
            [Required] public string FirstName { get; set; } = string.Empty;
            [Required] public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public EmployeeRole Role { get; set; }
            public DateTime HireDate { get; set; } = DateTime.Today;
            public bool IsActive { get; set; } = true;
        }
    }
}
