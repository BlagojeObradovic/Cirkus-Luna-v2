// Ansvarlig: Abraham
namespace CirkusLuna.ClassLibrary.Models
{
    public class Performance
    {
        // max kapacitet ifølge opgavebeskrivelsen
        public const int MaxRegularSeats = 150;
        public const int MaxVipSeats = 10;
        public const int TotalSeats = MaxRegularSeats + MaxVipSeats;

        public int Id { get; set; }
        public int CityId { get; set; }
        public CircusCity? City { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<Artist> Artists { get; set; } = new List<Artist>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Seat> Seats { get; set; } = new List<Seat>();

        // tæller reserverede pladser ved at løbe reservationerne igennem
        public int ReservedRegularSeats
        {
            get
            {
                int total = 0;
                for (int i = 0; i < Reservations.Count; i++)
                {
                    if (Reservations[i].TicketCategory != TicketCategory.VIP)
                        total += Reservations[i].NumberOfTickets;
                }
                return total;
            }
        }

        public int ReservedVipSeats
        {
            get
            {
                int total = 0;
                for (int i = 0; i < Reservations.Count; i++)
                {
                    if (Reservations[i].TicketCategory == TicketCategory.VIP)
                        total += Reservations[i].NumberOfTickets;
                }
                return total;
            }
        }

        public int AvailableRegularSeats
        {
            get { return MaxRegularSeats - ReservedRegularSeats; }
        }

        public int AvailableVipSeats
        {
            get { return MaxVipSeats - ReservedVipSeats; }
        }

        public bool HasAvailableSeats(TicketCategory category, int count)
        {
            if (category == TicketCategory.VIP)
                return ReservedVipSeats + count <= MaxVipSeats;
            return ReservedRegularSeats + count <= MaxRegularSeats;
        }

        public bool IsFuture
        {
            get { return Date.Date >= DateTime.Today; }
        }

        public string DateDisplay
        {
            get { return Date.ToString("dd. MMMM yyyy", new System.Globalization.CultureInfo("da-DK")); }
        }

        public string TimeDisplay
        {
            get { return StartTime.Hours.ToString("D2") + ":" + StartTime.Minutes.ToString("D2"); }
        }
    }
}
