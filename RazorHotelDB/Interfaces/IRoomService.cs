using RazorHotelDB.Models;
namespace RazorHotelDB.Interfaces {
    public interface IRoomService {
        Task<List<Room>> GetAllRoomAsync(int hotelNr);        
        Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr);       
        Task<bool> CreateRoomAsync(int hotelNr, Room room);       
        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);        
        Task<Room> DeleteRoomAsync(int roomNr, int hotelNr); 
    }
}
