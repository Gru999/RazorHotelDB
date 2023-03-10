using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Services;

namespace RazorHotelDB.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        private IHotelService _hotelService;

        public DeleteHotelModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task OnGetAsync(int id)
        {
            await _hotelService.GetHotelFromIdAsync(id);
        }
        public async Task OnPostAsync(int id) {
            await _hotelService.DeleteHotelAsync(id);
        }
    }
}
