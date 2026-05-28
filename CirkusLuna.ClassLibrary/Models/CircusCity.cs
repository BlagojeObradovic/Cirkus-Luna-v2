// Ansvarlig: Altay
namespace CirkusLuna.ClassLibrary.Models
{
    public class CircusCity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<Performance> Performances { get; set; } = new List<Performance>();

        public DateTime? FirstDate
        {
            get
            {
                if (Performances.Count == 0)
                    return null;

                DateTime earliest = Performances[0].Date;
                for (int i = 1; i < Performances.Count; i++)
                {
                    if (Performances[i].Date < earliest)
                        earliest = Performances[i].Date;
                }
                return earliest;
            }
        }

        public DateTime? LastDate
        {
            get
            {
                if (Performances.Count == 0)
                    return null;

                DateTime latest = Performances[0].Date;
                for (int i = 1; i < Performances.Count; i++)
                {
                    if (Performances[i].Date > latest)
                        latest = Performances[i].Date;
                }
                return latest;
            }
        }
    }
}
