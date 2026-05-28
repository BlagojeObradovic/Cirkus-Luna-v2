// Ansvarlig: Onur
namespace CirkusLuna.ClassLibrary.Models
{
    public enum SeatStatus
    {
        Available,
        Reserved
    }

    public class Seat
    {
        public int SeatNumber { get; set; }
        public int Row { get; set; }
        public int SeatInRow { get; set; }
        public bool IsVip { get; set; }
        public SeatStatus Status { get; set; } = SeatStatus.Available;
        public int? ReservationId { get; set; }

        public string DisplayName
        {
            get
            {
                return "R" + Row + "-" + SeatInRow;
            }
        }
    }
}
