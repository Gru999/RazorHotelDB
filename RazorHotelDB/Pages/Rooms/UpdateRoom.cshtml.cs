using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class UpdateRoomModel : PageModel
    {
        private IRoomService _roomService;

        [BindProperty]
        public Room Room { get; set; }

        [BindProperty]
        public RoomType RoomType { get; set; }

        public UpdateRoomModel(IRoomService roomService)
        {
            _roomService = roomService;
        }
        public async Task OnGetAsync(int hid, int rid)
        {
            Room = await _roomService.GetRoomFromIdAsync(rid, hid);
        }

        public async Task<IActionResult> OnPostAsync(int hid, int rid)
        {
            Room.Types = RoomType.ToString()[0];
            await _roomService.UpdateRoomAsync(Room, rid, hid);
            return RedirectToPage("GetAllRooms");
        }
    }
}
