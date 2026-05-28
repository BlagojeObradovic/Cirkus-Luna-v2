// Ansvarlig: Blagoje
namespace CirkusLuna.ClassLibrary.Exceptions
{
    public class CapacityExceededException : Exception
    {
        public CapacityExceededException()
            : base("Kapacitetsgrænsen for forestillingen er nået.") { }

        public CapacityExceededException(string ticketType, int max)
            : base($"Kapacitetsgrænsen for {ticketType}-pladser er nået. Maksimum er {max} pladser.") { }
    }
}
