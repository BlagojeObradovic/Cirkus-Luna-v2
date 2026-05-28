// Ansvarlig: Onur
namespace CirkusLuna.ClassLibrary.Models
{
    public class Customer : Person
    {
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
