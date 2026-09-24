namespace Eventify.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "Student";

    // New: stores the securely hashed password (never the real password).
    public string PasswordHash { get; set; } = "";

    public ICollection<UserInterest> Interests { get; set; } = [];
    public ICollection<Registration> Registrations { get; set; } = [];
}