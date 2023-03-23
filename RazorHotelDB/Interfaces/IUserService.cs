using RazorHotelDB.Models;

namespace RazorHotelDB.Interfaces
{
    public interface IUserService
    {
        public List<User> GetAllUsers();

        public User VerifyUser(string username, string password);
    }
}
