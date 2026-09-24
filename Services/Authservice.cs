using Eventify.Data;
using Eventify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services;

// Handles registering new users and checking login credentials.
// PasswordHasher applies the same secure hashing algorithm ASP.NET Core
// Identity itself uses internally - we get that security without needing
// to adopt the full Identity system.
public class AuthService(IDbContextFactory<EventifyDbContext> dbFactory)
{
    private readonly PasswordHasher<User> hasher = new();

    public async Task<(bool Success, string Error)> RegisterAsync(string fullName, string email, string password, string role)
    {
        await using var db = await dbFactory.CreateDbContextAsync();

        var exists = await db.Users.AnyAsync(u => u.Email == email);
        if (exists)
        {
            return (false, "An account with that email already exists.");
        }

        var user = new User
        {
            FullName = fullName,
            Email = email,
            Role = role,
        };
        user.PasswordHash = hasher.HashPassword(user, password);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return (true, "");
    }

    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        await using var db = await dbFactory.CreateDbContextAsync();

        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null)
        {
            return null;
        }

        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success ? user : null;
    }
}