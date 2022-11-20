using Gaines_Opus_Institute_Current.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Database for User
builder.Services.AddDbContext<GOSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GOSContext") ?? throw new InvalidOperationException("Connection string 'GOSContext' not found.")));

//Registering the Identity Services for User
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
        options => {
            options.SignIn.RequireConfirmedAccount = false;

            //Other options go here
        }
        )
    .AddEntityFrameworkStores<GOSContext>();

builder.Services.AddRazorPages();

//Stripe apiKey
StripeConfiguration.ApiKey = "sk_test_51KzqK9Hj2B2Quz911XrP11cB4Jb2ESrDCelSpRIZBqa18TWO9bGKlyuWsmiNeGYEHw4224xx5ghUWDaTQOukRjcf00rHXcZGYU";

//Setting options for Password, Lockout, and User
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+*#!";
    options.User.RequireUniqueEmail = true;

});

//Settings for Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

//Authentication
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

//Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Student",
            policy => policy.RequireClaim("User", "Student"));
    options.AddPolicy("Teacher",
            policy => policy.RequireClaim("User", "Teacher"));
    options.AddPolicy("Manager",
            policy => policy.RequireClaim("User", "Manager"));
});

//RazorPage Options
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Shared/_Layout2", "Student");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
