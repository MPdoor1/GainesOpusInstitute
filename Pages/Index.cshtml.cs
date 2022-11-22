using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gaines_Opus_Institute_Current.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var userRedirect = User != null && User.Identity.IsAuthenticated;
            if (userRedirect)
                {
                   return Redirect("/PagesLoggedIn/index2");
                }

            return Page();
        }
    }
}
