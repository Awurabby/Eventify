namespace Eventify.Services;

public class StubCurrentUserService : ICurrentUserService
{
    public bool IsAuthenticated { get; private set; }
    public int? UserId { get; private set; }
    public string? DisplayName { get; private set; }

    // Stub-only — lets us test the redirect flow before real auth exists.
    // The Login.razor stub page calls this directly.
    public void SimulateLogin()
    {
        IsAuthenticated = true;
        UserId = 1;
        DisplayName = "Demo Student";
    }

    public Task LogoutAsync()
    {
        IsAuthenticated = false;
        UserId = null;
        DisplayName = null;
        return Task.CompletedTask;
    }
}