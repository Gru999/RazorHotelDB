using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Hotels
{
    public class CreateHotelModel : PageModel
    {

        [BindProperty]
        public Hotel Hotel { get; set; }
        private IHotelService _hotelService;

        public CreateHotelModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync() {
            await _hotelService.CreateHotelAsync(Hotel);
            return RedirectToPage("GetAllHotels");
        }
    }
}
