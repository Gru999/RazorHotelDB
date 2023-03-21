using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class CreateRoomModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; }

        [BindProperty(SupportsGet = true)]
        public int HotelNr { get; set; }

        IRoomService roomService;
       

        [BindProperty]
        public RoomType RoomType { get; set; }

        [BindProperty]
        public bool createResult { get; set; }

        
        public CreateRoomModel(IRoomService rService)
        {
            this.roomService = rService;
            
        }
        public IActionResult OnGet(int id)
        {
            
            HotelNr = id;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Room.HotelNr = id;
            Room.Types = RoomType.ToString()[0];
            createResult = await roomService.CreateRoomAsync(HotelNr, Room);
            if (createResult) {
                return RedirectToPage("GetAllRooms");
                //return RedirectToPage("GetAllRooms", "MyRooms", new { cid = HotelNr });
                //spørg Poul om MyRooms
            }
            else {
                return Page();
            }


        }
    }
}
