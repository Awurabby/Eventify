using Eventify.Data;
using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services;

public class UserService
{
    private readonly IDbContextFactory<EventifyDbContext> _factory;

    public UserService(IDbContextFactory<EventifyDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<string>> GetUserInterestNamesAsync(int userId)
    {
        await using var db = await _factory.CreateDbContextAsync();

        return await db.UserInterests
            .Where(ui => ui.UserId == userId)
            .Select(ui => ui.Interest.Name)
            .ToListAsync();
    }

    public async Task SaveUserInterestsAsync(int userId, IEnumerable<string> interestNames)
    {
        await using var db = await _factory.CreateDbContextAsync();

        db.UserInterests.RemoveRange(db.UserInterests.Where(ui => ui.UserId == userId));

        var interests = await db.Interests
            .Where(i => interestNames.Contains(i.Name))
            .ToListAsync();

        foreach (var interest in interests)
        {
            db.UserInterests.Add(new UserInterest { UserId = userId, InterestId = interest.Id });
        }

        await db.SaveChangesAsync();
    }
}