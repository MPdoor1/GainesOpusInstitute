using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gaines_Opus_Institute_Current.Pages
{
    public class logoutModel : PageModel
    {
            public async Task<IActionResult> OnPostAsync()
           {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("~/index.cshtml");
        }
    }
}
