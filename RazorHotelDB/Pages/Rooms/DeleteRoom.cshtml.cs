using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class DeleteRoomModel : PageModel
    {
        private IRoomService _roomService;
        private IHotelService _hotelService;
        [BindProperty]
        public Room Room { get; set; }
        public Hotel Hotel { get; set; }

        public DeleteRoomModel(IRoomService roomService, IHotelService hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }
        public async Task OnGetAsync(int hid, int rid)
        {
            Room = await _roomService.GetRoomFromIdAsync(rid, hid);
        }
        public async Task<IActionResult> OnPostAsync(int hid, int rid)
        {
            await _roomService.DeleteRoomAsync(rid, hid);
            return RedirectToPage("GetAllRooms");
        }
    }
}
