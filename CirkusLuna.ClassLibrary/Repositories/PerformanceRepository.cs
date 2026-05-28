// Ansvarlig: Abraham
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Repositories
{
    public class PerformanceRepository : IPerformanceRepository
    {
        private readonly List<Performance> _performances = new List<Performance>();
        private int _nextId = 1;

        public PerformanceRepository(ICircusCityRepository cityRepo, IArtistRepository artistRepo)
        {
            List<CircusCity> cities = cityRepo.GetAll();
            List<Artist> artists = artistRepo.GetAll();

            SeedPerformance(cities[0], new DateTime(2026, 6, 5),  new TimeSpan(19, 0, 0),  "Åbningsforestilling i København – en aften fuld af magi og underholdning!", artists);
            SeedPerformance(cities[0], new DateTime(2026, 6, 6),  new TimeSpan(15, 0, 0),  "Eftermiddagsforestilling med ekstra klovnenumre for familien.", artists);
            SeedPerformance(cities[0], new DateTime(2026, 6, 6),  new TimeSpan(19, 30, 0), "Aftenforestilling med ildshow som afslutning.", artists);
            SeedPerformance(cities[1], new DateTime(2026, 6, 12), new TimeSpan(19, 0, 0),  "Cirkus Luna i Aarhus – stor premiere med fuldt program.", artists);
            SeedPerformance(cities[1], new DateTime(2026, 6, 13), new TimeSpan(14, 0, 0),  "Familieforestilling med særlig intro for de yngste.", artists);
            SeedPerformance(cities[2], new DateTime(2026, 6, 20), new TimeSpan(19, 0, 0),  "Cirkus Luna kommer til Odense for to dage!", artists);
            SeedPerformance(cities[2], new DateTime(2026, 6, 21), new TimeSpan(19, 0, 0),  "Afslutningsforestilling i Odense med hele trupens bedste numre.", artists);
            SeedPerformance(cities[3], new DateTime(2026, 7, 3),  new TimeSpan(19, 0, 0),  "Midsommer-forestilling i Aalborg under åben himmel.", artists);
            SeedPerformance(cities[4], new DateTime(2026, 7, 10), new TimeSpan(19, 30, 0), "Esbjerg byder velkommen til Cirkus Luna ved havnen.", artists);
            SeedPerformance(cities[5], new DateTime(2026, 7, 18), new TimeSpan(19, 0, 0),  "Cirkus Luna gæster Randers med et fantastisk program.", artists);
            SeedPerformance(cities[6], new DateTime(2026, 7, 25), new TimeSpan(19, 0, 0),  "Vejle-forestilling ved Vejle Fjord.", artists);
            SeedPerformance(cities[7], new DateTime(2026, 8, 1),  new TimeSpan(19, 0, 0),  "August-premiere i Horsens – sommerens højtidelige afslutning.", artists);
            SeedPerformance(cities[7], new DateTime(2026, 8, 2),  new TimeSpan(15, 0, 0),  "Sommermatinée i Horsens.", artists);
        }

        private void SeedPerformance(CircusCity city, DateTime date, TimeSpan time, string desc, List<Artist> artists)
        {
            Performance performance = new Performance
            {
                CityId = city.Id,
                City = city,
                Date = date,
                StartTime = time,
                Description = desc,
                IsActive = true
            };

            for (int i = 0; i < artists.Count; i++)
            {
                if (artists[i].IsPermanent)
                {
                    performance.Artists.Add(artists[i]);
                    artists[i].Performances.Add(performance);
                }
            }

            Add(performance);
            city.Performances.Add(performance);
        }

        public List<Performance> GetAll()
        {
            return _performances;
        }

        public Performance? GetById(int id)
        {
            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].Id == id)
                    return _performances[i];
            }
            return null;
        }

        public void Add(Performance performance)
        {
            performance.Id = _nextId++;
            GenerateSeats(performance);
            _performances.Add(performance);
        }

        public void Update(Performance performance)
        {
            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].Id == performance.Id)
                {
                    _performances[i] = performance;
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].Id == id)
                {
                    _performances.RemoveAt(i);
                    return;
                }
            }
        }

        public List<Performance> GetByCity(int cityId)
        {
            var results = new List<Performance>();

            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].CityId == cityId)
                    results.Add(_performances[i]);
            }

            return results;
        }

        public List<Performance> GetByDate(DateTime date)
        {
            var results = new List<Performance>();

            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].Date.Date == date.Date)
                    results.Add(_performances[i]);
            }

            return results;
        }

        public List<Performance> GetByDateRange(DateTime from, DateTime to)
        {
            var results = new List<Performance>();

            for (int i = 0; i < _performances.Count; i++)
            {
                var d = _performances[i].Date.Date;
                if (d >= from.Date && d <= to.Date)
                    results.Add(_performances[i]);
            }

            return results;
        }

        public List<Performance> GetFuture()
        {
            var results = new List<Performance>();

            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].Date.Date >= DateTime.Today)
                    results.Add(_performances[i]);
            }

            return results;
        }

        // bruges til filtrering på forsiden - sætter matches til false hvis ét filter ikke passer
        public List<Performance> Search(string? city, DateTime? date, TicketCategory? ticketCategory, bool? hasAvailableSeats)
        {
            var results = new List<Performance>();

            for (int i = 0; i < _performances.Count; i++)
            {
                var p = _performances[i];
                bool matches = true;

                if (!string.IsNullOrEmpty(city))
                {
                    if (p.City == null || !p.City.Name.ToLower().Contains(city.ToLower()))
                        matches = false;
                }

                if (date.HasValue && matches)
                {
                    if (p.Date.Date != date.Value.Date)
                        matches = false;
                }

                if (ticketCategory.HasValue && matches)
                {
                    if (ticketCategory.Value == TicketCategory.VIP)
                    {
                        if (p.AvailableVipSeats <= 0)
                            matches = false;
                    }
                    else
                    {
                        if (p.AvailableRegularSeats <= 0)
                            matches = false;
                    }
                }

                if (hasAvailableSeats.HasValue && matches)
                {
                    bool hasSeats = p.AvailableRegularSeats > 0 || p.AvailableVipSeats > 0;
                    if (hasAvailableSeats.Value != hasSeats)
                        matches = false;
                }

                if (matches)
                    results.Add(p);
            }

            return results;
        }

        // tjekker om artisten allerede er tilknyttet inden vi tilføjer
        public void AddArtistToPerformance(int performanceId, int artistId)
        {
            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].Id == performanceId)
                {
                    bool alreadyAdded = false;
                    for (int j = 0; j < _performances[i].Artists.Count; j++)
                    {
                        if (_performances[i].Artists[j].Id == artistId)
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }

                    if (!alreadyAdded)
                    {
                        _performances[i].Artists.Add(new Models.Artist { Id = artistId });
                    }
                    return;
                }
            }
        }

        public void RemoveArtistFromPerformance(int performanceId, int artistId)
        {
            for (int i = 0; i < _performances.Count; i++)
            {
                if (_performances[i].Id == performanceId)
                {
                    for (int j = 0; j < _performances[i].Artists.Count; j++)
                    {
                        if (_performances[i].Artists[j].Id == artistId)
                        {
                            _performances[i].Artists.RemoveAt(j);
                            return;
                        }
                    }
                }
            }
        }

        // VIP-rækken er altid de første 10 pladser, derefter 15 rækker à 10
        private void GenerateSeats(Performance performance)
        {
            performance.Seats.Clear();
            int seatNumber = 1;

            for (int s = 1; s <= Performance.MaxVipSeats; s++)
            {
                performance.Seats.Add(new Seat
                {
                    SeatNumber = seatNumber++,
                    Row = 0,
                    SeatInRow = s,
                    IsVip = true,
                    Status = SeatStatus.Available
                });
            }

            int regularRows = 15;
            int seatsPerRow = 10;

            for (int row = 1; row <= regularRows; row++)
            {
                for (int s = 1; s <= seatsPerRow; s++)
                {
                    performance.Seats.Add(new Seat
                    {
                        SeatNumber = seatNumber++,
                        Row = row,
                        SeatInRow = s,
                        IsVip = false,
                        Status = SeatStatus.Available
                    });
                }
            }
        }
    }
}
