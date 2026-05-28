// Ansvarlig: Onur
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly List<Artist> _artists = new List<Artist>();
        private int _nextId = 1;

        public ArtistRepository()
        {
            Add(new Artist { FirstName = "Marco", LastName = "Valentino", Email = "marco@cirkusluna.dk", Phone = "20123456", ArtistType = ArtistType.Acrobat, Nationality = "Italiensk", ActDescription = "Verdensmester i luftakrobatik med spektakulære stunts i 15 meters højde.", IsPermanent = true });
            Add(new Artist { FirstName = "Lena", LastName = "Müller", Email = "lena@cirkusluna.dk", Phone = "20234567", ArtistType = ArtistType.Clown, Nationality = "Tysk", ActDescription = "Den elskede klovn Luna med sin hund Bølle – garanteret latter til alle aldre.", IsPermanent = true });
            Add(new Artist { FirstName = "Ahmed", LastName = "Hassan", Email = "ahmed@cirkusluna.dk", Phone = "20345678", ArtistType = ArtistType.Juggler, Nationality = "Egyptisk", ActDescription = "Jonglerer med op til 9 fakler på én gang mens han balancerer på et enhjulet cykel.", IsPermanent = true });
            Add(new Artist { FirstName = "Sofia", LastName = "Petrov", Email = "sofia@cirkusluna.dk", Phone = "20456789", ArtistType = ArtistType.Aerialist, Nationality = "Russisk", ActDescription = "Luftakrobat i det silke – en poetisk og fantastisk forestilling.", IsPermanent = true });
            Add(new Artist { FirstName = "Carlos", LastName = "Ramirez", Email = "carlos@cirkusluna.dk", Phone = "20567890", ArtistType = ArtistType.FirePerformer, Nationality = "Spansk", ActDescription = "Ildkunstner og -æder med optræden til toner af flamenco-musik.", IsPermanent = false });
            Add(new Artist { FirstName = "Amalie", LastName = "Skov", Email = "amalie@cirkusluna.dk", Phone = "20678901", ArtistType = ArtistType.Magician, Nationality = "Dansk", ActDescription = "Tryllekunstner med moderne og interaktive numre der inddrager publikum.", IsPermanent = false });
            Add(new Artist { FirstName = "Dmitri", LastName = "Volkov", Email = "dmitri@cirkusluna.dk", Phone = "20789012", ArtistType = ArtistType.Acrobat, Nationality = "Russisk", ActDescription = "Vertikal pole og akrobatik med utrolig styrke og kontrol.", IsPermanent = true });
            Add(new Artist { FirstName = "Isabella", LastName = "Fernandez", Email = "isabella@cirkusluna.dk", Phone = "20890123", ArtistType = ArtistType.AnimalTrainer, Nationality = "Argentinsk", ActDescription = "Optræder med de trænede hesde i en ægte hesde-show.", IsPermanent = true });
        }

        public List<Artist> GetAll()
        {
            return _artists;
        }

        public Artist? GetById(int id)
        {
            for (int i = 0; i < _artists.Count; i++)
            {
                if (_artists[i].Id == id)
                    return _artists[i];
            }
            return null;
        }

        public void Add(Artist artist)
        {
            artist.Id = _nextId++;
            _artists.Add(artist);
        }

        public void Update(Artist artist)
        {
            for (int i = 0; i < _artists.Count; i++)
            {
                if (_artists[i].Id == artist.Id)
                {
                    _artists[i] = artist;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _artists.Count; i++)
            {
                if (_artists[i].Id == id)
                {
                    _artists.RemoveAt(i);
                    return;
                }
            }
        }

        // søger på tværs af navn, nationalitet og type
        public List<Artist> Search(string query)
        {
            var results = new List<Artist>();
            string lowerQuery = query.ToLower();

            for (int i = 0; i < _artists.Count; i++)
            {
                var a = _artists[i];
                if (a.FirstName.ToLower().Contains(lowerQuery) ||
                    a.LastName.ToLower().Contains(lowerQuery) ||
                    a.Nationality.ToLower().Contains(lowerQuery) ||
                    a.ArtistTypeDescription.ToLower().Contains(lowerQuery) ||
                    a.ActDescription.ToLower().Contains(lowerQuery))
                {
                    results.Add(a);
                }
            }

            return results;
        }

        public List<Artist> GetByType(ArtistType artistType)
        {
            var results = new List<Artist>();

            for (int i = 0; i < _artists.Count; i++)
            {
                if (_artists[i].ArtistType == artistType)
                    results.Add(_artists[i]);
            }

            return results;
        }

        public List<Artist> GetPermanent()
        {
            var results = new List<Artist>();

            for (int i = 0; i < _artists.Count; i++)
            {
                if (_artists[i].IsPermanent)
                    results.Add(_artists[i]);
            }

            return results;
        }

        // dobbelt løkke fordi artister har en liste af forestillinger
        public List<Artist> GetByPerformance(int performanceId)
        {
            var results = new List<Artist>();

            for (int i = 0; i < _artists.Count; i++)
            {
                var artist = _artists[i];
                for (int j = 0; j < artist.Performances.Count; j++)
                {
                    if (artist.Performances[j].Id == performanceId)
                    {
                        results.Add(artist);
                        break;
                    }
                }
            }

            return results;
        }
    }
}
