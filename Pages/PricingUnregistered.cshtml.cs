using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;

public class StripeOptions
{
    public string option { get; set; }
}

namespace Gaines_Opus_Institute_Current.Pages
{
    public class PricingUnregisteredModel : PageModel
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
        public ActionResult OnPost()
        {
            var domain = "http://localhost:44390";
            var pOption = Request.Form["PriceOption"];
            var priceOption = pOption.First();

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = priceOption,
                    Quantity = 1,
                },
                },
                Mode = "payment",
                SuccessUrl = domain + "/success",
                CancelUrl = domain + "/cancel",
                AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }   
    }
}
