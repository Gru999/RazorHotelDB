using Azure.Identity;
using RazorHotelDB.Data;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Services
{
    public class UserService : IUserService
    {
        public List<User> GetAllUsers()
        {
            return MockData.UserData;
        }

        public User VerifyUser(string username, string password)
        {
            foreach (var user in GetAllUsers()) {
                if (username.Equals(user.UserName) && password.Equals(user.Password)) {
                    return user;
                }
            }
            return null;
        }
    }
}
