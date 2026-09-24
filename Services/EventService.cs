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
}