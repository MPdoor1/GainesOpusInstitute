using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gaines_Opus_Institute_Current.Pages.PagesLoggedIn
{
    [Authorize(Policy = "BasicUser")]
    public class index2Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
