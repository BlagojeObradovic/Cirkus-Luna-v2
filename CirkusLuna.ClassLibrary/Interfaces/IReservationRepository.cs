// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetAll();
        Reservation? GetById(int id);
        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int id);
        List<Reservation> GetByCustomer(int customerId);
        List<Reservation> GetByPerformance(int performanceId);
        List<Reservation> Search(string query);
    }
}
