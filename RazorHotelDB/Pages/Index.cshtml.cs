using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorHotelDB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string UserName { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null)
            {
                return RedirectToPage("Login");
            }
            else
            {
                return Page();
            }
        }
    }
}