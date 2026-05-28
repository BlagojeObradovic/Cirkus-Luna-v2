// Ansvarlig: Onur
namespace CirkusLuna.ClassLibrary.Models
{
    public enum ArtistType
    {
        Acrobat,
        Clown,
        Juggler,
        Magician,
        AnimalTrainer,
        Aerialist,
        FirePerformer,
        Other
    }

    public class Artist : Person
    {
        public ArtistType ArtistType { get; set; }

        public string ArtistTypeDescription
        {
            get
            {
                switch (ArtistType)
                {
                    case ArtistType.Acrobat: return "Akrobat";
                    case ArtistType.Clown: return "Klovn";
                    case ArtistType.Juggler: return "Jonglør";
                    case ArtistType.Magician: return "Tryllekunstner";
                    case ArtistType.AnimalTrainer: return "Dyretræner";
                    case ArtistType.Aerialist: return "Luftakrobat";
                    case ArtistType.FirePerformer: return "Ildkunstner";
                    default: return "Andet";
                }
            }
        }

        public string Nationality { get; set; } = string.Empty;
        public string ActDescription { get; set; } = string.Empty;
        public bool IsPermanent { get; set; } = true;
        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
