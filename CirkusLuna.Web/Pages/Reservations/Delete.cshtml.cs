// Ansvarlig: Blagoje
using CirkusLuna.ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Web.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly ReservationService _service;
        public DeleteModel(ReservationService service)
        {
            _service = service;
        }

        public int ReservationId { get; set; }

        public IActionResult OnGet(int id)
        {
            ReservationId = id;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _service.CancelReservation(id);
                TempData["Success"] = "Reservation annulleret og pladser frigivet.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
    }
}
