// Ansvarlig: Blagoje
namespace CirkusLuna.ClassLibrary.Exceptions
{
    public class InvalidReservationException : Exception
    {
        public InvalidReservationException()
            : base("Reservationen er ugyldig.") { }

        public InvalidReservationException(string message)
            : base(message) { }
    }
}
