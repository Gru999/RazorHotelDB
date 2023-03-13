using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Hotels
{
    public class UpdateHotelModel : PageModel
    {
        private IHotelService _hotelService;

        [BindProperty]
        public Hotel Hotel { get; set; }

        public UpdateHotelModel(IHotelService hotelService) {
            _hotelService = hotelService;
        }
        public async Task OnGetAsync(int id) {
            Hotel = await _hotelService.GetHotelFromIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(int id) {
            await _hotelService.UpdateHotelAsync(Hotel, id);
            return RedirectToPage("GetAllHotels");
        }
    }
}
