using System.Security.Claims;
using Eventify.Dtos;
using Eventify.Models;
using Eventify.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    // GET /api/events
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();
        return Ok(events);
    }

    // GET /api/events/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        var eventItem = await _eventService.GetEventByIdAsync(id);
        return eventItem is null ? NotFound() : Ok(eventItem);
    }

    // POST /api/events/create-event
    [HttpPost("create-event")]
    [Authorize(Roles = "Organizer,Admin")]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var eventItem = new EventItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Date = dto.Date,
            Location = dto.Location,
            Capacity = dto.Capacity,
            OrganizerId = GetCurrentUserId(),
        };

        await _eventService.AddEventAsync(eventItem);
        return CreatedAtAction(nameof(GetEvent), new { id = eventItem.Id }, eventItem);
    }

    // PUT /api/events/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Organizer,Admin")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var changes = new EventItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Date = dto.Date,
            Location = dto.Location,
            Capacity = dto.Capacity
        };

        try
        {
            var updated = await _eventService.UpdateEventAsync(id, changes, GetCurrentUserId(), IsAdmin());
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    // DELETE /api/events/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Organizer,Admin")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        try
        {
            var deleted = await _eventService.DeleteEventAsync(id, GetCurrentUserId(), IsAdmin());
            return deleted ? NoContent() : NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    // TODO: confirm with Johanness which claim actually carries the user's
    // numeric ID in your JWT (this assumes ClaimTypes.NameIdentifier).
    private int GetCurrentUserId()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(idClaim!);
    }

    private bool IsAdmin() => User.IsInRole("Admin");
}
