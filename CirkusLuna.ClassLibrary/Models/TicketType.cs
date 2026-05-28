// Ansvarlig: Blagoje
namespace CirkusLuna.ClassLibrary.Models
{
    public enum TicketCategory
    {
        Regular,
        Children,
        VIP
    }

    public class TicketType
    {
        public int Id { get; set; }
        public TicketCategory Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public string CategoryName
        {
            get
            {
                switch (Category)
                {
                    case TicketCategory.Regular: return "Almindelig";
                    case TicketCategory.Children: return "Børnebillet";
                    case TicketCategory.VIP: return "VIP";
                    default: return "Ukendt";
                }
            }
        }
    }
}
