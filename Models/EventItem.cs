namespace Eventify.Models;

public class EventItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public DateTime Date { get; set; }
    public string Location { get; set; } = "";
    public string Organizer { get; set; } = "";
    public int Capacity { get; set; }
}
