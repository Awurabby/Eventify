using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Eventify.Services;

// The real version of ICurrentUserService. It reads who is logged in from the
// login cookie, so it always matches what /login and /register set up.
public class CurrentUserService(
    AuthenticationStateProvider authStateProvider,
    NavigationManager navigation) : ICurrentUserService
{
    // In Blazor Server the login state is already known when the page loads,
    // so this task is already finished and reading it is instant.
    private ClaimsPrincipal? User
    {
        get
        {
            var task = authStateProvider.GetAuthenticationStateAsync();
            return task.IsCompletedSuccessfully ? task.Result.User : null;
        }
    }

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;

    public int? UserId =>
        int.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

    public string? DisplayName => User?.Identity?.Name;

    public string? Role => User?.FindFirst(ClaimTypes.Role)?.Value;

    // Logging out means clearing the cookie, which needs a normal web request,
    // so we send the browser to the logout endpoint and let it do that.
    public Task LogoutAsync()
    {
        navigation.NavigateTo("/account/logout", forceLoad: true);
        return Task.CompletedTask;
    }
}