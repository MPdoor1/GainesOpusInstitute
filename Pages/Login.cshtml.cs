using Gaines_Opus_Institute_Current.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Gaines_Opus_Institute_Current.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }
        
        [BindProperty]
        public User user { get; set; } = default!;

        public void OnGet()
        {

            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(User user)
        {


            var result = await _signInManager.PasswordSignInAsync(user.username,
                           user.password, user.rememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
               
                _logger.LogInformation("User logged in.");


                //Create the security context
                var claims = new List<Claim>();
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    return RedirectToPage("/PagesLoggedIn/index2");
                }
                else
                {
                    return Page();
                }
        }

    }
}
