using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;
using RazorHotelDB.Services;

namespace RazorHotelDB.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        private IRoomService _roomService;
        private IHotelService _hotelService;

        //HotelNr only bind to @Model.HotelNr and then never used
        [BindProperty(SupportsGet = true)]
        public int HotelNr { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Room> Rooms { get; set; }

        public GetAllRoomsModel(IRoomService roomService, IHotelService hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
            Hotels = new List<Hotel>();
        }

        public async Task<IActionResult> OnGetAsync(int hid)
        {
            
            Hotels = await _hotelService.GetAllHotelAsync();
            if (hid != 0)
            {
                Rooms = await _roomService.GetAllRoomAsync(hid);
            }
            return Page();
        }

        public async Task OnPostAsync(int hid)
        {
            Rooms = await _roomService.GetAllRoomAsync(hid);
            Hotels = await _hotelService.GetAllHotelAsync(); 
        }
    }
}
