using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Shared
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        private IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }


        public void OnGet()
        {
        }

        public void OnGetLogout() {
            HttpContext.Session.Remove("UserName");
        }

        public IActionResult OnPost() {
            User loginUser = _userService.VerifyUser(UserName, Password);
            if (loginUser != null)
            {
                HttpContext.Session.SetString("UserName", loginUser.UserName);
                return RedirectToPage("Index");
            }
            else {
                Message = "Invalid Username or Password";
                UserName = "";
                Password = "";
                return Page();
            }
        }
    }
}
