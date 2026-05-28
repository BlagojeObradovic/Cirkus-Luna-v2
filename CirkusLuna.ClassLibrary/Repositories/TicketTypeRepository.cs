// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly List<TicketType> _ticketTypes = new List<TicketType>();
        private int _nextId = 1;

        public TicketTypeRepository()
        {
            Add(new TicketType { Category = TicketCategory.Regular, Name = "Voksen", Price = 175, Description = "Almindelig billet for voksne. Inkluderer sæde i den store ring." });
            Add(new TicketType { Category = TicketCategory.Children, Name = "Børn (0-12 år)", Price = 95, Description = "Børnebillet for børn op til 12 år." });
            Add(new TicketType { Category = TicketCategory.VIP, Name = "VIP", Price = 350, Description = "VIP-billet med de bedste pladser og adgang til eksklusive oplevelser før forestillingen." });
        }

        public List<TicketType> GetAll()
        {
            return _ticketTypes;
        }

        public TicketType? GetById(int id)
        {
            for (int i = 0; i < _ticketTypes.Count; i++)
            {
                if (_ticketTypes[i].Id == id)
                    return _ticketTypes[i];
            }
            return null;
        }

        public void Add(TicketType ticketType)
        {
            ticketType.Id = _nextId++;
            _ticketTypes.Add(ticketType);
        }

        public void Update(TicketType ticketType)
        {
            for (int i = 0; i < _ticketTypes.Count; i++)
            {
                if (_ticketTypes[i].Id == ticketType.Id)
                {
                    _ticketTypes[i] = ticketType;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _ticketTypes.Count; i++)
            {
                if (_ticketTypes[i].Id == id)
                {
                    _ticketTypes.RemoveAt(i);
                    return;
                }
            }
        }

        public TicketType? GetByCategory(TicketCategory category)
        {
            for (int i = 0; i < _ticketTypes.Count; i++)
            {
                if (_ticketTypes[i].Category == category)
                    return _ticketTypes[i];
            }
            return null;
        }
    }
}
