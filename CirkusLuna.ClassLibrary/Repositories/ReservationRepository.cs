// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly List<Reservation> _reservations = new List<Reservation>();
        private int _nextId = 1;

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation? GetById(int id)
        {
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Id == id)
                    return _reservations[i];
            }
            return null;
        }

        public void Add(Reservation reservation)
        {
            reservation.Id = _nextId++;
            _reservations.Add(reservation);
        }

        public void Update(Reservation reservation)
        {
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Id == reservation.Id)
                {
                    _reservations[i] = reservation;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Id == id)
                {
                    _reservations.RemoveAt(i);
                    return;
                }
            }
        }

        public List<Reservation> GetByCustomer(int customerId)
        {
            var results = new List<Reservation>();

            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].CustomerId == customerId)
                    results.Add(_reservations[i]);
            }

            return results;
        }

        public List<Reservation> GetByPerformance(int performanceId)
        {
            var results = new List<Reservation>();

            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].PerformanceId == performanceId)
                    results.Add(_reservations[i]);
            }

            return results;
        }

        public List<Reservation> Search(string query)
        {
            var results = new List<Reservation>();
            string lowerQuery = query.ToLower();

            for (int i = 0; i < _reservations.Count; i++)
            {
                var r = _reservations[i];
                bool matches = false;

                if (r.Customer != null)
                {
                    if (r.Customer.FullName.ToLower().Contains(lowerQuery) ||
                        r.Customer.Email.ToLower().Contains(lowerQuery))
                        matches = true;
                }

                if (!matches && r.Performance?.City != null)
                {
                    if (r.Performance.City.Name.ToLower().Contains(lowerQuery))
                        matches = true;
                }

                if (matches)
                    results.Add(r);
            }

            return results;
        }
    }
}
