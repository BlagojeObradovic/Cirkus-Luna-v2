// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Exceptions;
using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Models;
using CirkusLuna.ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CirkusLuna.Web.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly ReservationService _service;
        private readonly ICustomerRepository _customerRepo;
        private readonly IPerformanceRepository _performanceRepo;
        private readonly ITicketTypeRepository _ticketTypeRepo;

        public CreateModel(
            ReservationService service,
            ICustomerRepository customerRepo,
            IPerformanceRepository performanceRepo,
            ITicketTypeRepository ticketTypeRepo)
        {
            _service = service;
            _customerRepo = customerRepo;
            _performanceRepo = performanceRepo;
            _ticketTypeRepo = ticketTypeRepo;
        }

        [BindProperty]
        public ReservationInput Input { get; set; } = new();

        public List<Customer> Customers { get; set; } = new();
        public List<Performance> FuturePerformances { get; set; } = new();
        public List<TicketType> TicketTypes { get; set; } = new();

        public void OnGet(int? performanceId)
        {
            LoadData();
            if (performanceId.HasValue)
                Input.PerformanceId = performanceId.Value;
        }

        public IActionResult OnPost()
        {
            LoadData();

            if (!ModelState.IsValid) return Page();

            try
            {
                _service.CreateReservation(
                    Input.CustomerId,
                    Input.PerformanceId,
                    Input.TicketTypeId,
                    Input.NumberOfTickets);

                TempData["Success"] = "Reservationen er gennemført!";
                return RedirectToPage("Index");
            }
            catch (PastPerformanceException ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
            catch (NoAvailableSeatsException ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
            catch (CapacityExceededException ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
            catch (InvalidReservationException ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }

        private void LoadData()
        {
            Customers = _customerRepo.GetAll();
            FuturePerformances = _performanceRepo.GetFuture();
            TicketTypes = _ticketTypeRepo.GetAll();
        }

        public class ReservationInput
        {
            [Required(ErrorMessage = "Vælg en kunde")]
            [Range(1, int.MaxValue, ErrorMessage = "Vælg en kunde")]
            public int CustomerId { get; set; }

            [Required(ErrorMessage = "Vælg en forestilling")]
            [Range(1, int.MaxValue, ErrorMessage = "Vælg en forestilling")]
            public int PerformanceId { get; set; }

            [Required(ErrorMessage = "Vælg en billettype")]
            [Range(1, int.MaxValue, ErrorMessage = "Vælg en billettype")]
            public int TicketTypeId { get; set; }

            [Required(ErrorMessage = "Angiv antal billetter")]
            [Range(1, 20, ErrorMessage = "Antal billetter skal være mellem 1 og 20")]
            public int NumberOfTickets { get; set; } = 1;
        }
    }
}
