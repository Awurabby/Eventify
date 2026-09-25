namespace Eventify.Services;

public interface ICurrentUserService
{
    bool IsAuthenticated { get; }
    int? UserId { get; }
    string? DisplayName { get; }
}