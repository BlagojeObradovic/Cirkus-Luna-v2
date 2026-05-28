// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer? GetById(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
        List<Customer> Search(string query);
        List<Customer> GetByCity(string city);
    }
}
