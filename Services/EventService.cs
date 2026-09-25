using Eventify.Data;
using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services;

public class EventService
{
    private readonly IDbContextFactory<EventifyDbContext> _factory;

    public EventService(IDbContextFactory<EventifyDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<EventItem>> GetEventsAsync()
    {
        await using var db = await _factory.CreateDbContextAsync();

        return await db.Events
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    public async Task<EventItem?> GetEventByIdAsync(int id)
    {
        await using var db = await _factory.CreateDbContextAsync();

        return await db.Events
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddEventAsync(EventItem eventItem)
    {
        await using var db = await _factory.CreateDbContextAsync();

        db.Events.Add(eventItem);
        await db.SaveChangesAsync();
    }

    // Applies validated field changes onto an existing event.
    // Throws UnauthorizedAccessException if the caller doesn't own the event and isn't an admin.
    public async Task<EventItem?> UpdateEventAsync(int id, EventItem changes, int requestingUserId, bool isAdmin)
    {
        await using var db = await _factory.CreateDbContextAsync();

        var existing = await db.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (existing is null) return null;

        if (!isAdmin && existing.OrganizerId != requestingUserId)
        {
            throw new UnauthorizedAccessException("You cannot edit another organizer's event.");
        }

        existing.Title = changes.Title;
        existing.Description = changes.Description;
        existing.Category = changes.Category;
        existing.Date = changes.Date;
        existing.Location = changes.Location;
        existing.Capacity = changes.Capacity;

        await db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteEventAsync(int id, int requestingUserId, bool isAdmin)
    {
        await using var db = await _factory.CreateDbContextAsync();

        var existing = await db.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (existing is null) return false;

        // TODO: same OrganizerId dependency as UpdateEventAsync above.
        if (!isAdmin && existing.OrganizerId != requestingUserId)
        {
            throw new UnauthorizedAccessException("You cannot delete another organizer's event.");
        }

        db.Events.Remove(existing);
        await db.SaveChangesAsync();
        return true;
    }
}
