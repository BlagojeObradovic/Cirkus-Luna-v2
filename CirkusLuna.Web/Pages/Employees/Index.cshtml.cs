// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeRepository _repo;
        public IndexModel(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public List<Employee> Employees { get; set; } = new();
        public string? Query { get; set; }

        public void OnGet(string? query)
        {
            Query = query;
            var all = string.IsNullOrWhiteSpace(query) ? _repo.GetAll() : _repo.Search(query);
            Employees = all.ToList();
        }
    }
}
