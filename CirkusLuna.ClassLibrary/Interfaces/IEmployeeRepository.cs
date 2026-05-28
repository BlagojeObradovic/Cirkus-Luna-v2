// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee? GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
        List<Employee> Search(string query);
        List<Employee> GetByRole(EmployeeRole role);
        List<Employee> GetActive();
    }
}
