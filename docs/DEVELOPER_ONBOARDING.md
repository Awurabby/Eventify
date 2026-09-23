# Developer Onboarding

## 1. Install

Install the .NET 10 SDK.

Verify:

```bash
dotnet --version
```

The major version should be 10.

## 2. Clone

```bash
git clone YOUR_GITHUB_REPOSITORY_URL
cd Eventify
```

## 3. Restore

```bash
dotnet restore
```

## 4. Run

```bash
dotnet run
```

Open the HTTPS URL printed by the application.

## 5. Understand the first files

Start with:

1. `Program.cs`
2. `Eventify.csproj`
3. `Components/App.razor`
4. `Components/Layout/MainLayout.razor`
5. `Components/Pages/Home.razor`
6. `Models/EventItem.cs`
7. `Data/EventifyDbContext.cs`
8. `Services/EventService.cs`

Do not edit files randomly. Understand the ownership of the feature you are assigned.

## 6. Before asking for help

Send:

- exact command you ran
- exact error message
- file name
- relevant code
- what you expected to happen

Do not send passwords, API keys or connection-string secrets.

## 7. If the build breaks

First run:

```bash
dotnet clean
dotnet restore
dotnet build
```

Then check the first error, not the last error.

## 8. Database

The starter defaults to SQLite so the application can be run without installing a database server.

The team can move to SQL Server once the core application is stable. Keep provider-specific changes isolated in `Program.cs` and configuration.
