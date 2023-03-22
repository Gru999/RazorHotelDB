using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;
using System.Xml.Linq;

namespace RazorHotelDB.Services {
    public class HotelService : Connection, IHotelService {
        private string queryString = "select * from Hotel";

        private string queryFilter = "select * from Hotel where Name like @Name";

        private string queryCreate = "insert into Hotel Values(@ID, @Navn, @Adresse)";

        private string queryDelete = "delete from Booking where Hotel_No = @HotelNr;" +
                                    "delete from Room where Hotel_No = @HotelNr;" +
                                    "delete from Hotel where Hotel_No = @HotelNr;";

        private string queryUpdate = "update Hotel set Hotel_No = @HotelNr, Name = @Navn, Address = @Adresse where Hotel_No = @HotelNr;";

        private string queryFilterId = "select * from Hotel where Hotel_No = @ID";

        public HotelService(IConfiguration configuration) : base(configuration) {
        }

        public async Task<bool> CreateHotelAsync(Hotel hotel) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    SqlCommand command = new SqlCommand(queryCreate, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    await command.Connection.OpenAsync();
                    int result = await command.ExecuteNonQueryAsync();
                    return result == 1;
                }
                catch (SqlException sqlEx) {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex) {
                    Console.WriteLine("Generel error " + ex.Message);
                }
            }
            return false;
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryDelete, connection);
                    command.Parameters.AddWithValue("@HotelNr", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        return await GetHotelFromIdAsync(hotelNr);
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

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();//aSynkront
                        SqlDataReader reader = await command.ExecuteReaderAsync();//aSynkront                        
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                        return null;
                    }
                }
            }
            return hoteller;
        }

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryFilterId, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int hotelNo = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel h = new Hotel(hotelNo, hotelNavn, hotelAdr);
                        return h;
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

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name) {
            List<Hotel> hotels = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryFilter, connection);
                    string nameWildcard = "%" + name + "%";
                    commmand.Parameters.AddWithValue("@Name", nameWildcard);
                    await commmand.Connection.OpenAsync();
                    SqlDataReader reader = await commmand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32(0);
                        string hotelName = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelName, hotelAdr);
                        hotels.Add(hotel);
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
                return hotels;
            }
            return null;
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryUpdate, connection);                    
                    command.Parameters.AddWithValue("HotelNr", hotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    await command.Connection.OpenAsync();
                    int updated = await command.ExecuteNonQueryAsync();
                    return updated == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                    throw ex;
                }
            }
            return false;
        }
    }
}
