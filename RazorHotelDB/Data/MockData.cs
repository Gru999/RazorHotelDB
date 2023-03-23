using RazorHotelDB.Models;

namespace RazorHotelDB.Data
{
    public class MockData
    {
        public static List<User> UserData = new List<User> {
            new User { Id = 1, UserName = "Admin", Password = "password" }, 
            new User { Id = 2, UserName = "Alice", Password = "1234" }, 
            new User { Id = 3, UserName = "Bob", Password = "1234" }
        };
    }
}
