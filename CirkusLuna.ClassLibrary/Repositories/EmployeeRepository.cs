// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new List<Employee>();
        private int _nextId = 1;

        public EmployeeRepository()
        {
            Add(new Employee { FirstName = "Jens", LastName = "Larsen", Email = "jens@cirkusluna.dk", Phone = "30111222", Role = EmployeeRole.Manager, HireDate = new DateTime(2018, 1, 15), IsActive = true });
            Add(new Employee { FirstName = "Marie", LastName = "Hansen", Email = "marie@cirkusluna.dk", Phone = "30222333", Role = EmployeeRole.Cashier, HireDate = new DateTime(2020, 3, 1), IsActive = true });
            Add(new Employee { FirstName = "Peter", LastName = "Nielsen", Email = "peter@cirkusluna.dk", Phone = "30333444", Role = EmployeeRole.Technician, HireDate = new DateTime(2019, 5, 10), IsActive = true });
            Add(new Employee { FirstName = "Anna", LastName = "Christensen", Email = "anna@cirkusluna.dk", Phone = "30444555", Role = EmployeeRole.Stagehand, HireDate = new DateTime(2021, 4, 20), IsActive = true });
            Add(new Employee { FirstName = "Lars", LastName = "Andersen", Email = "lars@cirkusluna.dk", Phone = "30555666", Role = EmployeeRole.Security, HireDate = new DateTime(2022, 2, 1), IsActive = true });
        }

        public List<Employee> GetAll()
        {
            return _employees;
        }

        public Employee? GetById(int id)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].Id == id)
                    return _employees[i];
            }
            return null;
        }

        public void Add(Employee employee)
        {
            employee.Id = _nextId++;
            _employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].Id == employee.Id)
                {
                    _employees[i] = employee;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].Id == id)
                {
                    _employees.RemoveAt(i);
                    return;
                }
            }
        }

        public List<Employee> Search(string query)
        {
            var results = new List<Employee>();
            string lowerQuery = query.ToLower();

            for (int i = 0; i < _employees.Count; i++)
            {
                var e = _employees[i];
                if (e.FirstName.ToLower().Contains(lowerQuery) ||
                    e.LastName.ToLower().Contains(lowerQuery) ||
                    e.Email.ToLower().Contains(lowerQuery) ||
                    e.RoleDescription.ToLower().Contains(lowerQuery))
                {
                    results.Add(e);
                }
            }

            return results;
        }

        public List<Employee> GetByRole(EmployeeRole role)
        {
            var results = new List<Employee>();

            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].Role == role)
                    results.Add(_employees[i]);
            }

            return results;
        }

        public List<Employee> GetActive()
        {
            var results = new List<Employee>();

            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].IsActive)
                    results.Add(_employees[i]);
            }

            return results;
        }
    }
}
