// Ansvarlig: Blagoje
namespace CirkusLuna.ClassLibrary.Exceptions
{
    // bruges hvis nogen prøver at booke en forestilling der allerede er afholdt
    public class PastPerformanceException : Exception
    {
        public PastPerformanceException()
            : base("Du kan ikke reservere billetter til en forestilling, der allerede er afholdt.") { }

        public PastPerformanceException(DateTime performanceDate)
            : base($"Forestillingen den {performanceDate:dd. MMMM yyyy} er allerede afholdt. Reservationer er kun mulige til fremtidige forestillinger.") { }
    }
}
