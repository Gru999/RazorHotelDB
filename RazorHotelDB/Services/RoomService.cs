using Microsoft.Data.SqlClient;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Services
{
    public class RoomService : Connection, IRoomService
    {
        private string queryString = "select * from Room where Hotel_No = @hotelNr";


        public RoomService(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            throw new NotImplementedException();
        }

        public Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Room>> GetAllRoomAsync(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@hotelNr", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        char type = reader.GetString(2).First();
                        double price = reader.GetDouble(3);
                        Room r = new Room(roomNo, hotelNo, type, price);
                        rooms.Add(r);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return rooms;
        }

        public Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }
    }
}
