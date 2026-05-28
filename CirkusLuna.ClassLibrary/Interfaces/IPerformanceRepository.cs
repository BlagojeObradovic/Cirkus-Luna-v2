// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Interfaces
{
    public interface IPerformanceRepository
    {
        List<Performance> GetAll();
        Performance? GetById(int id);
        void Add(Performance performance);
        void Update(Performance performance);
        void Delete(int id);
        List<Performance> GetByCity(int cityId);
        List<Performance> GetByDate(DateTime date);
        List<Performance> GetByDateRange(DateTime from, DateTime to);
        List<Performance> GetFuture();
        List<Performance> Search(string? city, DateTime? date, TicketCategory? ticketCategory, bool? hasAvailableSeats);
        void AddArtistToPerformance(int performanceId, int artistId);
        void RemoveArtistFromPerformance(int performanceId, int artistId);
    }
}
