using System.ComponentModel.DataAnnotations;

namespace Eventify.Dtos;

// Used for POST /create-event
public class CreateEventDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required, MaxLength(100)]
    public string Category { get; set; } = "";

    [Required]
    public DateTime Date { get; set; }

    [Required, MaxLength(200)]
    public string Location { get; set; } = "";

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1")]
    public int Capacity { get; set; }

    // NOTE: OrganizerId is deliberately NOT here.
    // It must never come from client input - it's set server-side
    // from the authenticated user's claims in the controller/service.
}

// Used for PUT /events/{id}
public class UpdateEventDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required, MaxLength(100)]
    public string Category { get; set; } = "";

    [Required]
    public DateTime Date { get; set; }

    [Required, MaxLength(200)]
    public string Location { get; set; } = "";

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1")]
    public int Capacity { get; set; }
}

// What we hand back to the frontend
public class EventResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public DateTime Date { get; set; }
    public string Location { get; set; } = "";
    public int OrganizerId { get; set; }
    public int Capacity { get; set; }
    public bool IsUpcoming => Date >= DateTime.UtcNow;
}
