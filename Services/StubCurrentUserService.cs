namespace Eventify.Services;

public class StubCurrentUserService : ICurrentUserService
{
    public bool IsAuthenticated { get; private set; }
    public int? UserId { get; private set; }
    public string? DisplayName { get; private set; }
    public string? Role { get; private set; }

    // Stub-only — lets us test flows before real auth exists.
    // Login.razor calls this. Change "Student" to "Organizer" here
    // to preview the organizer-dashboard experience.
    public void SimulateLogin(string role = "Student")
    {
        IsAuthenticated = true;
        UserId = 1;
        DisplayName = "Demo Student";
        Role = role;
    }

    public Task LogoutAsync()
    {
        IsAuthenticated = false;
        UserId = null;
        DisplayName = null;
        Role = null;
        return Task.CompletedTask;
    }
}