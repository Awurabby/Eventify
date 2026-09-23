using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Data;

public class EventifyDbContext(DbContextOptions<EventifyDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Interest> Interests => Set<Interest>();
    public DbSet<UserInterest> UserInterests => Set<UserInterest>();
    public DbSet<EventItem> Events => Set<EventItem>();
    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInterest>()
            .HasKey(x => new { x.UserId, x.InterestId });

        modelBuilder.Entity<UserInterest>()
            .HasOne(x => x.User)
            .WithMany(x => x.Interests)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<UserInterest>()
            .HasOne(x => x.Interest)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.InterestId);

        modelBuilder.Entity<Registration>()
            .HasIndex(x => new { x.UserId, x.EventId })
            .IsUnique();
    }
}
