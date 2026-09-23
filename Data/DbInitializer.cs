using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IDbContextFactory<EventifyDbContext> factory)
    {
        await using var db = await factory.CreateDbContextAsync();
        await db.Database.EnsureCreatedAsync();

        if (!await db.Interests.AnyAsync())
        {
            db.Interests.AddRange(
                new Interest { Name = "Technology" },
                new Interest { Name = "Career" },
                new Interest { Name = "Sports" },
                new Interest { Name = "Music" },
                new Interest { Name = "Entrepreneurship" },
                new Interest { Name = "Academic" });

            await db.SaveChangesAsync();
        }

        if (!await db.Events.AnyAsync())
        {
            db.Events.AddRange(
                new EventItem
                {
                    Title = "Campus Tech Meetup",
                    Description = "A practical session on modern software development and technology careers.",
                    Category = "Technology",
                    Date = DateTime.Today.AddDays(3),
                    Location = "New Lecture Theatre",
                    Organizer = "Computing Society",
                    Capacity = 120
                },
                new EventItem
                {
                    Title = "Career Development Workshop",
                    Description = "CV, interview and internship preparation for university students.",
                    Category = "Career",
                    Date = DateTime.Today.AddDays(6),
                    Location = "Business School Auditorium",
                    Organizer = "Career Services",
                    Capacity = 200
                },
                new EventItem
                {
                    Title = "Student Entrepreneurship Forum",
                    Description = "Meet student founders and learn how campus ideas become real ventures.",
                    Category = "Entrepreneurship",
                    Date = DateTime.Today.AddDays(9),
                    Location = "Innovation Hub",
                    Organizer = "Entrepreneurship Club",
                    Capacity = 150
                });

            await db.SaveChangesAsync();
        }
    }
}
