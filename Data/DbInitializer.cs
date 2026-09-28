using Eventify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Data;

public static class DbInitializer
{
    // Demo accounts for local development and the class demo only.
    // These are fake accounts on a local test database, not real credentials.
    private const string DemoPassword = "Demo@1234";

    public static async Task InitializeAsync(IDbContextFactory<EventifyDbContext> factory)
    {
        await using var db = await factory.CreateDbContextAsync();
        await db.Database.MigrateAsync();

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

        // Creates each demo user if missing, and gives the old Demo Student
        // a real password if it doesn't have one yet.
        await EnsureDemoUserAsync(db, "Demo Student", "demo.student@eventify.test", "Student");
        await EnsureDemoUserAsync(db, "Demo Organizer", "demo.organizer@eventify.test", "Organizer");
        await EnsureDemoUserAsync(db, "Demo Admin", "demo.admin@eventify.test", "Admin");

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

    private static async Task EnsureDemoUserAsync(EventifyDbContext db, string fullName, string email, string role)
    {
        var hasher = new PasswordHasher<User>();
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            user = new User { FullName = fullName, Email = email, Role = role };
            user.PasswordHash = hasher.HashPassword(user, DemoPassword);
            db.Users.Add(user);
        }
        else if (string.IsNullOrEmpty(user.PasswordHash))
        {
            user.PasswordHash = hasher.HashPassword(user, DemoPassword);
        }

        await db.SaveChangesAsync();
    }
}