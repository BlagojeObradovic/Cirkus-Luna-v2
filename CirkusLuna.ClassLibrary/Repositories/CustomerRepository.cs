// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new List<Customer>();
        private int _nextId = 1;

        public CustomerRepository()
        {
            Add(new Customer { FirstName = "Thomas", LastName = "Eriksen", Email = "thomas@example.dk", Phone = "40111222", Address = "Rosenvej 5", City = "København", PostalCode = "2100" });
            Add(new Customer { FirstName = "Sara", LastName = "Møller", Email = "sara@example.dk", Phone = "40222333", Address = "Birkevej 12", City = "Aarhus", PostalCode = "8000" });
            Add(new Customer { FirstName = "Mikkel", LastName = "Dahl", Email = "mikkel@example.dk", Phone = "40333444", Address = "Egevej 3", City = "Odense", PostalCode = "5000" });
            Add(new Customer { FirstName = "Louise", LastName = "Sørensen", Email = "louise@example.dk", Phone = "40444555", Address = "Lindevej 8", City = "Aalborg", PostalCode = "9000" });
            Add(new Customer { FirstName = "Christian", LastName = "Holm", Email = "christian@example.dk", Phone = "40555666", Address = "Pilevej 2", City = "Esbjerg", PostalCode = "6700" });
        }

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetById(int id)
        {
            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i].Id == id)
                    return _customers[i];
            }
            return null;
        }

        public void Add(Customer customer)
        {
            customer.Id = _nextId++;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i].Id == customer.Id)
                {
                    _customers[i] = customer;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i].Id == id)
                {
                    _customers.RemoveAt(i);
                    return;
                }
            }
        }

        public List<Customer> Search(string query)
        {
            var results = new List<Customer>();
            string lowerQuery = query.ToLower();

            for (int i = 0; i < _customers.Count; i++)
            {
                var c = _customers[i];
                if (c.FirstName.ToLower().Contains(lowerQuery) ||
                    c.LastName.ToLower().Contains(lowerQuery) ||
                    c.Email.ToLower().Contains(lowerQuery) ||
                    c.Phone.Contains(query) ||
                    c.City.ToLower().Contains(lowerQuery))
                {
                    results.Add(c);
                }
            }

            return results;
        }

        public List<Customer> GetByCity(string city)
        {
            var results = new List<Customer>();
            string lowerCity = city.ToLower();

            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i].City.ToLower().Contains(lowerCity))
                    results.Add(_customers[i]);
            }

            return results;
        }
    }
}
