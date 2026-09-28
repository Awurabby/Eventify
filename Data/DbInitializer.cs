using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Data;

public static class DbInitializer
{
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
        if (!await db.Users.AnyAsync())
        {
            db.Users.Add(new User
                {
                    Id = 1,
                    FullName = "Demo Student",
                    Email = "demo.student@eventify.test",
                    Role = "Student"
                });

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
                },
                new EventItem
                {
                    Title = "Inter-Hall Football Finals",
                    Description = "Championship match between the top two halls this season.",
                    Category = "Sports",
                    Date = DateTime.Today.AddDays(12),
                    Location = "Legon Sports Stadium",
                    Organizer = "Sports Directorate",
                    Capacity = 2000
                },
                new EventItem
                {
                    Title = "Basketball 3v3 Tournament",
                    Description = "Fast-paced 3v3 tournament open to all students.",
                    Category = "Sports",
                    Date = DateTime.Today.AddDays(-2),
                    Location = "Indoor Sports Complex",
                    Organizer = "Sports Directorate",
                    Capacity = 120
                },
                new EventItem
                {
                    Title = "Acoustic Night at the Quad",
                    Description = "Live acoustic performances from student musicians.",
                    Category = "Music",
                    Date = DateTime.Today.AddDays(4),
                    Location = "Central Quad",
                    Organizer = "Music Society",
                    Capacity = 300
                },
                new EventItem
                {
                    Title = "Mid-Semester Research Symposium",
                    Description = "Undergraduate and graduate students present ongoing research.",
                    Category = "Academic",
                    Date = DateTime.Today.AddDays(-10),
                    Location = "Jones Quartey Building",
                    Organizer = "Office of Research and Innovation",
                    Capacity = 250
                },
                new EventItem
                {
                    Title = "AI & Machine Learning Workshop",
                    Description = "Intro to practical ML with hands-on coding exercises.",
                    Category = "Technology",
                    Date = DateTime.Today.AddDays(20),
                    Location = "Computer Science Dept. Lab 2",
                    Organizer = "Computing Society",
                    Capacity = 80
                },
                new EventItem
                {
                    Title = "Resume & LinkedIn Clinic",
                    Description = "One-on-one resume reviews and LinkedIn profile optimization.",
                    Category = "Career",
                    Date = DateTime.Today.AddDays(-5),
                    Location = "Career Services Centre",
                    Organizer = "Career Services",
                    Capacity = 60
                },
                new EventItem
                {
                    Title = "Pitch Deck Teardown Session",
                    Description = "Get live feedback on your startup pitch deck from mentors.",
                    Category = "Entrepreneurship",
                    Date = DateTime.Today.AddDays(15),
                    Location = "Innovation Hub",
                    Organizer = "Entrepreneurship Club",
                    Capacity = 50
                });

            await db.SaveChangesAsync();
        
        }
    }
}
