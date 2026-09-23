namespace Eventify.Models;

public class Registration
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int EventId { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public bool Attended { get; set; }
}
