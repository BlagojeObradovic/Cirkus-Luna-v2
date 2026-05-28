// Ansvarlig: Altay
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class CircusCityRepository : ICircusCityRepository
    {
        private readonly List<CircusCity> _cities = new List<CircusCity>();
        private int _nextId = 1;

        public CircusCityRepository()
        {
            Add(new CircusCity { Name = "København", Venue = "Fælledparken", Address = "Fælledparken 1, 2100 København Ø" });
            Add(new CircusCity { Name = "Aarhus", Venue = "Brabrandstien", Address = "Brabrandstien 5, 8210 Aarhus V" });
            Add(new CircusCity { Name = "Odense", Venue = "Munkebjerg", Address = "Munkebjerg 10, 5230 Odense M" });
            Add(new CircusCity { Name = "Aalborg", Venue = "Kildeparken", Address = "Kildeparken 2, 9000 Aalborg" });
            Add(new CircusCity { Name = "Esbjerg", Venue = "Strandparken", Address = "Strandparken 3, 6700 Esbjerg" });
            Add(new CircusCity { Name = "Randers", Venue = "Nørreport", Address = "Nørreport 15, 8900 Randers" });
            Add(new CircusCity { Name = "Vejle", Venue = "Vejle Idrætsanlæg", Address = "Stadionvej 1, 7100 Vejle" });
            Add(new CircusCity { Name = "Horsens", Venue = "Bygholm Park", Address = "Bygholm Park 4, 8700 Horsens" });
        }

        public List<CircusCity> GetAll()
        {
            return _cities;
        }

        public CircusCity? GetById(int id)
        {
            for (int i = 0; i < _cities.Count; i++)
            {
                if (_cities[i].Id == id)
                    return _cities[i];
            }
            return null;
        }

        public void Add(CircusCity city)
        {
            city.Id = _nextId++;
            _cities.Add(city);
        }

        public void Update(CircusCity city)
        {
            for (int i = 0; i < _cities.Count; i++)
            {
                if (_cities[i].Id == city.Id)
                {
                    _cities[i] = city;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _cities.Count; i++)
            {
                if (_cities[i].Id == id)
                {
                    _cities.RemoveAt(i);
                    return;
                }
            }
        }

        // Insertion sort - vi måtte ikke bruge List.Sort()
        public List<CircusCity> GetSortedAlphabetically()
        {
            var sorted = new List<CircusCity>(_cities);

            for (int i = 1; i < sorted.Count; i++)
            {
                var key = sorted[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(sorted[j].Name, key.Name,
                    StringComparison.OrdinalIgnoreCase) > 0)
                {
                    sorted[j + 1] = sorted[j];
                    j--;
                }

                sorted[j + 1] = key;
            }

            return sorted;
        }

        public List<CircusCity> Search(string query)
        {
            var results = new List<CircusCity>();
            string lowerQuery = query.ToLower();

            for (int i = 0; i < _cities.Count; i++)
            {
                var city = _cities[i];
                if (city.Name.ToLower().Contains(lowerQuery) ||
                    city.Venue.ToLower().Contains(lowerQuery) ||
                    city.Address.ToLower().Contains(lowerQuery))
                {
                    results.Add(city);
                }
            }

            return results;
        }
    }
}
