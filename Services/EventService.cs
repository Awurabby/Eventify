using Eventify.Data;
using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services;

public class EventService
{
    private readonly IDbContextFactory<EventifyDbContext> _factory;
    private readonly RecommendationService _recommendationService;

    public EventService(IDbContextFactory<EventifyDbContext> factory, RecommendationService recommendationService)
    {
        _factory = factory;
        _recommendationService = recommendationService;
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

        // TODO: swap to existing.OrganizerId once Asuako Joel adds that column.
        // Currently EventItem only has `Organizer` (string), which isn't safe
        // to compare against a numeric user id - this line won't compile
        // until that field exists.
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

    // GET /api/events/recommended
    // Fetches the raw data, then delegates the actual matching logic to
    // RecommendationService so that logic stays unit-testable on its own.
    public async Task<List<EventItem>> GetRecommendedEventsAsync(int userId)
    {
        await using var db = await _factory.CreateDbContextAsync();

        // TODO: confirm the DbSet is actually named "UserInterests" in
        // EventifyDbContext - adjust if it's called something else.
        var interestNames = await db.UserInterests
            .Where(ui => ui.UserId == userId)
            .Select(ui => ui.Interest.Name)
            .ToListAsync();

        if (interestNames.Count == 0)
        {
            // No interests set yet - nothing to recommend against.
            return new List<EventItem>();
        }

        var upcomingEvents = await db.Events
            .Where(e => e.Date >= DateTime.UtcNow)
            .ToListAsync();

        return _recommendationService.Recommend(upcomingEvents, interestNames).ToList();
    }
}
