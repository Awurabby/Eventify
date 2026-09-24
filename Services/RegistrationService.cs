using Eventify.Data;
using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services;

public class RegistrationService
{
    private readonly IDbContextFactory<EventifyDbContext> _factory;

    public RegistrationService(IDbContextFactory<EventifyDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<bool> RegisterAsync(int userId, int eventId)
    {
        await using var db = await _factory.CreateDbContextAsync();

        var alreadyRegistered = await db.Registrations
            .AnyAsync(r => r.UserId == userId && r.EventId == eventId);

        if (alreadyRegistered)
            return false;

        db.Registrations.Add(new Registration
        {
            UserId = userId,
            EventId = eventId,
            RegisteredAt = DateTime.UtcNow,
            Attended = false
        });

        await db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IsRegisteredAsync(int userId, int eventId)
    {
        await using var db = await _factory.CreateDbContextAsync();

        return await db.Registrations
            .AnyAsync(r => r.UserId == userId && r.EventId == eventId);
    }

    public async Task<List<Registration>> GetUserRegistrationsAsync(int userId)
    {
        await using var db = await _factory.CreateDbContextAsync();

        return await db.Registrations
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.RegisteredAt)
            .ToListAsync();
    }
}