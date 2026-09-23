# Eventify Architecture

## High-level

Eventify is a monolithic ASP.NET Core Blazor Web App.

```text
Browser
   |
   v
Blazor UI / Razor Components
   |
   v
C# Application Services
   |
   +---- Entity Framework Core ----> SQLite (local) / SQL Server (production option)
   |
   +---- SignalR ------------------> Real-time notifications
```

## Why monolithic?

The project has a short deadline and requires a single deployment for the simplest submission path. A single ASP.NET Core application reduces frontend/backend deployment and CORS integration work.

## Main folders

- `Components/Pages` — routable UI pages
- `Components/Layout` — shared application shell/navigation
- `Models` — domain entities
- `Data` — EF Core DbContext and database initialization
- `Services` — business/application logic
- `wwwroot` — CSS and static assets
- `docs` — team documentation

## Planned domain

```text
User
 |
 +--- UserInterest --- Interest

User
 |
 +--- Registration --- Event

User
 |
 +--- Notification
```

## Recommendation logic

The first version should use deterministic matching rather than ML:

```text
User interests:
Technology, Career

Event categories:
Technology

=> recommended
```

This is easy to explain in a final presentation and easy to test.
