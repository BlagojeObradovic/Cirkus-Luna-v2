// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _repo;
        public IndexModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public List<Customer> Customers { get; set; } = new();
        public string? Query { get; set; }

        public void OnGet(string? query)
        {
            Query = query;

            var all = string.IsNullOrWhiteSpace(query)
                ? _repo.GetAll()
                : _repo.Search(query);

            Customers = all.ToList();
        }
    }
}
