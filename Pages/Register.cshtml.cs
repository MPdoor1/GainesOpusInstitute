using Gaines_Opus_Institute_Current.Data;
using Gaines_Opus_Institute_Current.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Gaines_Opus_Institute_Current.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly GOSContext _db;

        public User user { get; set; }

        public RegisterModel(GOSContext db, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(User user)
        {

            if (ModelState.IsValid)
            {
                await _db.user.AddAsync(user);
                await _db.SaveChangesAsync();

                var IUser = new IdentityUser { UserName = user.username, Email = user.email};
                var result = await _userManager.CreateAsync(IUser, user.password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //Create the security context
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim("User", "Student")
                    };

                    //Sets the date of register
                    user.EnrollmentDate = DateTime.Now;

                   // var IdeUser = await _userManager.FindByNameAsync(user.username);
                    await _userManager.AddClaimsAsync(IUser, claims);
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(IUser);
                    await _signInManager.RefreshSignInAsync(IUser);

                    await _signInManager.SignInAsync(IUser, user.rememberMe);
                    return RedirectToPage("/PagesLoggedIn/index2");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            else
            {
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
