using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gaines_Opus_Institute_Current.Pages.indexHome
{
    //[Authorize(Policy = "BasicUser")]
    public class contactModel : PageModel
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
