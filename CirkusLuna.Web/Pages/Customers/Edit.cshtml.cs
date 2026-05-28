// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _repo;
        public EditModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public CustomerEditInput Customer { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var c = _repo.GetById(id);
            if (c == null) return NotFound();

            Customer = new CustomerEditInput
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                City = c.City,
                PostalCode = c.PostalCode
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var existing = _repo.GetById(Customer.Id);
            if (existing == null) return NotFound();

            existing.FirstName = Customer.FirstName;
            existing.LastName = Customer.LastName;
            existing.Email = Customer.Email;
            existing.Phone = Customer.Phone;
            existing.Address = Customer.Address;
            existing.City = Customer.City;
            existing.PostalCode = Customer.PostalCode;

            _repo.Update(existing);
            TempData["Success"] = "Kunde opdateret!";
            return RedirectToPage("Index");
        }

        public class CustomerEditInput
        {
            public int Id { get; set; }
            [Required(ErrorMessage = "Fornavn er påkrævet")]
            public string FirstName { get; set; } = string.Empty;
            [Required(ErrorMessage = "Efternavn er påkrævet")]
            public string LastName { get; set; } = string.Empty;
            [Required(ErrorMessage = "Email er påkrævet"), EmailAddress]
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string PostalCode { get; set; } = string.Empty;
        }
    }
}
