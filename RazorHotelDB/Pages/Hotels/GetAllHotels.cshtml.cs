using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;
using System.Runtime.CompilerServices;

namespace RazorHotelDB.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; set; }
        private IHotelService _hotelService;

        public GetAllHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task OnGetAsync()
        {
            if (!FilterCriteria.IsNullOrEmpty()) {
                Hotels = await _hotelService.GetHotelsByNameAsync(FilterCriteria);
            }
            else {
                Hotels = await _hotelService.GetAllHotelAsync();
            }
        }
    }
}
