using Eventify.Components;
using Eventify.Data;
using Eventify.Endpoints;
using Eventify.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// NEW: lets Blazor components know who's logged in.
builder.Services.AddCascadingAuthenticationState();

// NEW: cookie-based login. When someone isn't logged in and tries to view
// a page that requires it, they get sent to /login automatically.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
    });

builder.Services.AddAuthorization();

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
builder.Services.AddScoped<AuthService>();   // NEW

builder.Services.AddSignalR();

builder.Services.AddScoped<StubCurrentUserService>();
builder.Services.AddScoped<ICurrentUserService>(sp => sp.GetRequiredService<StubCurrentUserService>());

builder.Services.AddScoped<UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// NEW: these two lines must come before UseAntiforgery and before the
// app starts mapping pages/endpoints.
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<NotificationHub>("/hubs/notifications");
app.MapAccountEndpoints();   // NEW: turns on /account/login, /account/register, /account/logout

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<EventifyDbContext>>();
    await DbInitializer.InitializeAsync(factory);
}

app.Run();