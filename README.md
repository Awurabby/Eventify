# Eventify

Eventify is a monolithic campus event discovery and management web application built on the .NET ecosystem.

## Current architecture

- **.NET 10**
- **ASP.NET Core**
- **Blazor Web App (Interactive Server)**
- **C#**
- **Entity Framework Core**
- **SQLite for local development**
- **SignalR foundation**
- **Bootstrap + custom CSS**

The submitted proposal originally listed ASP.NET Core Web API, Blazor WebAssembly, EF Core, SQL Server, SignalR and Docker. The implementation has been intentionally simplified into one deployable monolithic Blazor Web App so the team can reach a stable working submission within the short project window.

## Run locally

Install the .NET 10 SDK first.

```bash
dotnet restore
dotnet run
```

Open the HTTPS URL shown by the terminal.

## Important

The current repository is a **starter/MVP skeleton**, not the finished submission. The sample UI and in-memory registration behavior exist so the team can immediately see the application running while the real EF Core database, authentication, authorization, event CRUD, recommendations and SignalR features are implemented.

## Team workflow

1. Create a branch from `main`.
2. Work on one clearly scoped task.
3. Make small commits with meaningful messages.
4. Push your branch.
5. Open a pull request.
6. At least one teammate reviews the PR.
7. Merge only when the feature builds and does not break the app.

See `docs/TEAM_WORK_GUIDE.md` and `docs/10_DAY_EXECUTION_PLAN.md`.
