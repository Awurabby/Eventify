using System.Security.Claims;
using Eventify.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Endpoints;

// Plain HTTP endpoints (not Blazor components). Signing a user in/out means
// setting a cookie on the HTTP response, which only a normal request/response
// can do - an interactive Blazor page (connected over SignalR) can't.
public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        app.MapPost("/account/login", async (
            HttpContext http,
            AuthService authService,
            [FromForm] string email,
            [FromForm] string password,
            [FromForm] string? returnUrl) =>
        {
            var user = await authService.ValidateCredentialsAsync(email, password);
            if (user is null)
            {
                return Results.Redirect("/login?error=1");
            }

            await SignInUserAsync(http, user.Id, user.FullName, user.Email, user.Role);

            return Results.LocalRedirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        });

        app.MapPost("/account/register", async (
            HttpContext http,
            AuthService authService,
            [FromForm] string fullName,
            [FromForm] string email,
            [FromForm] string password,
            [FromForm] string role) =>
        {
            var (success, error) = await authService.RegisterAsync(fullName, email, password, role);
            if (!success)
            {
                return Results.Redirect($"/register?error={Uri.EscapeDataString(error)}");
            }

            var user = await authService.ValidateCredentialsAsync(email, password);
            await SignInUserAsync(http, user!.Id, user.FullName, user.Email, user.Role);

            return Results.LocalRedirect("/");
        });

        app.MapPost("/account/logout", async (HttpContext http) =>
        {
            await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Results.LocalRedirect("/");
        });
    }

    private static Task SignInUserAsync(HttpContext http, int userId, string fullName, string email, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Name, fullName),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, role),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        return http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}