namespace Eventify.Services;

public class RegistrationService
{
    private readonly HashSet<string> registrations = [];

    public void Register(string userKey, int eventId)
        => registrations.Add(Key(userKey, eventId));

    public bool IsRegistered(string userKey, int eventId)
        => registrations.Contains(Key(userKey, eventId));

    private static string Key(string userKey, int eventId) => $"{userKey}:{eventId}";
}
