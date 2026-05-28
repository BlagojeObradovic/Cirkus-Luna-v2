// Ansvarlig: Blagoje
namespace CirkusLuna.ClassLibrary.Exceptions
{
    // kastes når der ikke er nok ledige pladser til det ønskede antal
    public class NoAvailableSeatsException : Exception
    {
        public NoAvailableSeatsException()
            : base("Der er ikke nok ledige pladser til denne forestilling.") { }

        public NoAvailableSeatsException(string message)
            : base(message) { }

        public NoAvailableSeatsException(int requested, int available)
            : base($"Der er ikke nok ledige pladser. Du ønsker {requested} billetter, men der er kun {available} ledige.") { }
    }
}
