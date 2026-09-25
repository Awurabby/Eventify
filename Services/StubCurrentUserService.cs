namespace Eventify.Services;

public class StubCurrentUserService : ICurrentUserService
{
    // TEMPORARY — flip to true to preview the logged-in experience
    // before the real auth branch is merged.
    private const bool SimulateLoggedIn = false;

    public bool IsAuthenticated => SimulateLoggedIn;
    public int? UserId => SimulateLoggedIn ? 1 : null;
    public string? DisplayName => SimulateLoggedIn ? "Demo Student" : null;
}