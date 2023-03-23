namespace RazorHotelDB.Interfaces
{
    public class ILoginService
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsloggedIn { get; set; }
    }
}
