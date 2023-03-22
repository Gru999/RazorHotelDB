using Microsoft.Data.SqlClient;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Services
{
    public class RoomService : Connection, IRoomService
    {

        /// <summary>
        /// Query strings containing SQL commands that are parsed into the database through the methods
        /// </summary>
        private string queryString = "select * from Room where Hotel_No = @hotelNr";
        private string queryCreate = "if exists (select * from Room where Hotel_No = @hotelNr) " +
                                     "begin insert into Room (Room_No, Hotel_No, Types, Price) values (@roomNr, @hotelNr, @type, @price); " +
                                     "end";
        private string queryDelete = "if exists (select * from Room where Hotel_No = @hotelNr and Room_No = @roomNr) " +
                                     "begin delete from Booking where Hotel_No = @hotelNr and Room_No = @roomNr " +
                                     "delete from Room where Hotel_No = @hotelNr and Room_No = @roomNr " +
                                     "end";
        private string queryRoomFromId = "select * from Room where Room_No = @roomNr and Hotel_No = @hotelNr;";
        private string queryUpdate = "update Room set Room_No = @roomNr, Hotel_No = @hotelNr, Types = @type, Price = @pris where Room_No = @roomNo and Hotel_No = @hotelNo;";


        public RoomService(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// The method creates a new room record in the database
        /// </summary>
        /// <param name="hotelNr">Indicates which hotel the new room is "connected" to</param>
        /// <param name="room">The new room we want to add</param>
        /// <returns>1/true if the method executed as intended and false if it failed</returns>
        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryCreate, connection);
                    command.Parameters.AddWithValue("@roomNr", room.RoomNr);
                    command.Parameters.AddWithValue("@hotelNr", room.HotelNr);
                    command.Parameters.AddWithValue("@type", room.Types);
                    command.Parameters.AddWithValue("@price", room.Pris);
                    await command.Connection.OpenAsync();
                    int result = await command.ExecuteNonQueryAsync();
                    return result == 1;
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
            return false;
        }


        /// <summary>
        /// The method deletes a room accross all the tables in the database
        /// </summary>
        /// <param name="roomNr">Used to find the room with the given room number</param>
        /// <param name="hotelNr">Used to find the hotel that has the specific room</param>
        /// <returns>The corresponding room if the method executed as intended or returns null if it fails</returns>
        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryDelete, connection);
                    command.Parameters.AddWithValue("@roomNr", roomNr);
                    command.Parameters.AddWithValue("@hotelNr", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        return await GetRoomFromIdAsync(roomNr, hotelNr);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel error " + ex.Message);
                }
            }
            return null;
        }


        /// <summary>
        /// The method gets a list of rooms from the given hotelNr
        /// </summary>
        /// <param name="hotelNr">Used to specify which hotel the wanted rooms are "connected" to</param>
        /// <returns>A list of the desired rooms if the method executes correctly, else it throws an exception and return an empty list</returns>
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


        /// <summary>
        /// The method retrieves the specific room from the database
        /// </summary>
        /// <param name="roomNr">Used to find the room with the given room number</param>
        /// <param name="hotelNr">Used to find the hotel that has the specific room</param>
        /// <returns>The desired room if it executes correctly, or returns null if it fails</returns>
        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryRoomFromId, connection);
                    command.Parameters.AddWithValue("@roomNr", roomNr);
                    command.Parameters.AddWithValue("@hotelNr", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        char type = reader.GetString(2).First();
                        double price = reader.GetDouble(3);
                        Room r = new Room(roomNo, hotelNo, type, price);
                        return r;
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
            return null;
        }


        /// <summary>
        /// The method updates an existing room in the database
        /// </summary>
        /// <param name="room">A room containing the values we want to update our old room with</param>
        /// <param name="roomNr">Used to find the room with the given room number</param>
        /// <param name="hotelNr">Used to find the hotel that has the specific room</param>
        /// <returns>1/true if the method executed as intended and false if it failed</returns>
        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryUpdate, connection);

                    //Get by int
                    command.Parameters.AddWithValue("@roomNo", roomNr);
                    command.Parameters.AddWithValue("@hotelNo", hotelNr);

                    //Values for update
                    command.Parameters.AddWithValue("@roomNr", room.RoomNr);
                    command.Parameters.AddWithValue("@hotelNr", room.HotelNr);
                    command.Parameters.AddWithValue("@type", room.Types);
                    command.Parameters.AddWithValue("@pris", room.Pris);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    int updated = await command.ExecuteNonQueryAsync();
                    return updated == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel error " + ex.Message);
                }
            }
            return false;
        }
    }
}
