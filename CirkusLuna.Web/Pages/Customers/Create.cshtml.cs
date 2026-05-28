// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _repo;
        public CreateModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public CustomerInput Customer { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _repo.Add(new Customer
            {
                FirstName = Customer.FirstName,
                LastName = Customer.LastName,
                Email = Customer.Email,
                Phone = Customer.Phone,
                Address = Customer.Address,
                City = Customer.City,
                PostalCode = Customer.PostalCode
            });

            TempData["Success"] = "Kunde oprettet!";
            return RedirectToPage("Index");
        }

        public class CustomerInput
        {
            [Required(ErrorMessage = "Fornavn er påkrævet")]
            public string FirstName { get; set; } = string.Empty;
            [Required(ErrorMessage = "Efternavn er påkrævet")]
            public string LastName { get; set; } = string.Empty;
            [Required(ErrorMessage = "Email er påkrævet"), EmailAddress(ErrorMessage = "Ugyldig email")]
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string PostalCode { get; set; } = string.Empty;
        }
    }
}
