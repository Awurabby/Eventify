using Eventify.Components;
using Eventify.Data;
using Eventify.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=eventify.db";

builder.Services.AddDbContextFactory<EventifyDbContext>(options =>
{
    // SQLite is the default for fast local development.
    // The provider can be switched to SQL Server in one place when the team is ready.
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<RecommendationService>();
builder.Services.AddScoped<RegistrationService>();

builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<NotificationHub>("/hubs/notifications");

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<EventifyDbContext>>();
    await DbInitializer.InitializeAsync(factory);
}

app.Run();
