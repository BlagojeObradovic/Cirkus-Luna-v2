// Ansvarlig: Blagoje
namespace CirkusLuna.ClassLibrary.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int PerformanceId { get; set; }
        public Performance? Performance { get; set; }
        public int TicketTypeId { get; set; }
        public TicketType? TicketTypeInfo { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public int NumberOfTickets { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public List<int> SeatNumbers { get; set; } = new List<int>();

        public decimal TotalPrice
        {
            get
            {
                if (TicketTypeInfo == null)
                    return 0;
                return TicketTypeInfo.Price * NumberOfTickets;
            }
        }
    }
}
