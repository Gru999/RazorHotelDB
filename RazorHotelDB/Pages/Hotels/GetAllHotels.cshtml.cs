using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;
using System.Runtime.CompilerServices;
using RazorHotelDB.Services;

namespace RazorHotelDB.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; set; }
        public string UserName { get; set; }
        private IHotelService _hotelService;

        public GetAllHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task<IActionResult> OnGetAsync(string sortOrder)
        {
            UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null)
            {
                return RedirectToPage("/Login");
            }
            try
            {
                if (!FilterCriteria.IsNullOrEmpty())
                {
                    Hotels = await _hotelService.GetHotelsByNameAsync(FilterCriteria);
                }
                else
                {
                    Hotels = await _hotelService.GetAllHotelAsync();
                }

                switch (sortOrder) {
                    case "name":
                        Hotels = Hotels.OrderBy(h => h.Navn).ToList();
                        break;
                    case "address":
                        Hotels = Hotels.OrderBy(h => h.Adresse).ToList();
                        break;
                    default:
                        Hotels = Hotels.OrderBy(h => h.HotelNr).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Hotels = new List<Models.Hotel>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
