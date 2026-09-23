using Eventify.Models;

namespace Eventify.Services;

public class EventService
{
    private readonly List<EventItem> sampleEvents =
    [
        new EventItem { Id = 1, Title = "Campus Tech Meetup", Description = "A practical session on modern software development and technology careers.", Category = "Technology", Date = DateTime.Today.AddDays(3), Location = "New Lecture Theatre", Organizer = "Computing Society", Capacity = 120 },
        new EventItem { Id = 2, Title = "Career Development Workshop", Description = "CV, interview and internship preparation for university students.", Category = "Career", Date = DateTime.Today.AddDays(6), Location = "Business School Auditorium", Organizer = "Career Services", Capacity = 200 },
        new EventItem { Id = 3, Title = "Student Entrepreneurship Forum", Description = "Meet student founders and learn how campus ideas become real ventures.", Category = "Entrepreneurship", Date = DateTime.Today.AddDays(9), Location = "Innovation Hub", Organizer = "Entrepreneurship Club", Capacity = 150 },
        new EventItem { Id = 4, Title = "Inter-Hall Sports Festival", Description = "A day of friendly competition across campus halls.", Category = "Sports", Date = DateTime.Today.AddDays(12), Location = "University Sports Grounds", Organizer = "Sports Directorate", Capacity = 500 }
    ];

    public IEnumerable<EventItem> GetSampleEvents() => sampleEvents;
}
