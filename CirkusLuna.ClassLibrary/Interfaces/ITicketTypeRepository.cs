// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface ITicketTypeRepository
    {
        List<TicketType> GetAll();
        TicketType? GetById(int id);
        void Add(TicketType ticketType);
        void Update(TicketType ticketType);
        void Delete(int id);
        TicketType? GetByCategory(TicketCategory category);
    }
}
