// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Exceptions;
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;

namespace CirkusLuna.ClassLibrary.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly IPerformanceRepository _performanceRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly ITicketTypeRepository _ticketTypeRepo;

        public ReservationService(
            IReservationRepository reservationRepo,
            IPerformanceRepository performanceRepo,
            ICustomerRepository customerRepo,
            ITicketTypeRepository ticketTypeRepo)
        {
            _reservationRepo = reservationRepo;
            _performanceRepo = performanceRepo;
            _customerRepo = customerRepo;
            _ticketTypeRepo = ticketTypeRepo;
        }

        // validerer alt inden vi gemmer - rækkefølgen er vigtig
        public Reservation CreateReservation(int customerId, int performanceId, int ticketTypeId, int numberOfTickets)
        {
            var performance = _performanceRepo.GetById(performanceId);
            if (performance == null)
            {
                throw new InvalidReservationException("Forestillingen blev ikke fundet.");
            }

            var customer = _customerRepo.GetById(customerId);
            if (customer == null)
            {
                throw new InvalidReservationException("Kunden blev ikke fundet.");
            }

            var ticketType = _ticketTypeRepo.GetById(ticketTypeId);
            if (ticketType == null)
            {
                throw new InvalidReservationException("Billettypen blev ikke fundet.");
            }

            if (numberOfTickets <= 0)
                throw new InvalidReservationException("Antallet af billetter skal være mindst 1.");

            if (!performance.IsFuture)
                throw new PastPerformanceException(performance.Date);

            if (!ValidateReservation(performanceId, ticketType.Category, numberOfTickets))
            {
                int available = performance.AvailableRegularSeats;
                if (ticketType.Category == TicketCategory.VIP)
                {
                    available = performance.AvailableVipSeats;
                }

                throw new NoAvailableSeatsException(numberOfTickets, available);
            }

            var reservation = new Reservation
            {
                CustomerId = customerId,
                Customer = customer,
                PerformanceId = performanceId,
                Performance = performance,
                TicketTypeId = ticketTypeId,
                TicketTypeInfo = ticketType,
                TicketCategory = ticketType.Category,
                NumberOfTickets = numberOfTickets,
                ReservationDate = DateTime.Now
            };

            AssignSeats(reservation, performance);

            _reservationRepo.Add(reservation);
            performance.Reservations.Add(reservation);

            return reservation;
        }

        public void CancelReservation(int reservationId)
        {
            var reservation = _reservationRepo.GetById(reservationId);
            if (reservation == null)
            {
                throw new InvalidReservationException("Reservationen blev ikke fundet.");
            }

            var performance = _performanceRepo.GetById(reservation.PerformanceId);
            if (performance != null)
            {
                for (int i = 0; i < performance.Reservations.Count; i++)
                {
                    if (performance.Reservations[i].Id == reservationId)
                    {
                        performance.Reservations.RemoveAt(i);
                        break;
                    }
                }

                for (int i = 0; i < reservation.SeatNumbers.Count; i++)
                {
                    int seatNum = reservation.SeatNumbers[i];
                    for (int j = 0; j < performance.Seats.Count; j++)
                    {
                        if (performance.Seats[j].SeatNumber == seatNum)
                        {
                            performance.Seats[j].Status = SeatStatus.Available;
                            performance.Seats[j].ReservationId = null;
                            break;
                        }
                    }
                }
            }

            _reservationRepo.Delete(reservationId);
        }

        public bool ValidateReservation(int performanceId, TicketCategory category, int numberOfTickets)
        {
            var performance = _performanceRepo.GetById(performanceId);
            if (performance == null) return false;

            return performance.HasAvailableSeats(category, numberOfTickets);
        }

        // går igennem sæderne og reserverer de første ledige af den rigtige type
        private void AssignSeats(Reservation reservation, Performance performance)
        {
            int seatsToAssign = reservation.NumberOfTickets;
            int assigned = 0;

            for (int i = 0; i < performance.Seats.Count && assigned < seatsToAssign; i++)
            {
                var seat = performance.Seats[i];

                if (seat.Status == SeatStatus.Available &&
                    seat.IsVip == (reservation.TicketCategory == TicketCategory.VIP))
                {
                    seat.Status = SeatStatus.Reserved;
                    seat.ReservationId = reservation.Id;
                    reservation.SeatNumbers.Add(seat.SeatNumber);
                    assigned++;
                }
            }
        }
    }
}
