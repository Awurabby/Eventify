# Eventify Team Work Guide

## Rule 1 — Do not work directly on `main`

Create a feature branch:

```bash
git checkout main
git pull
git checkout -b feature/your-feature-name
```

Examples:

- `feature/authentication`
- `feature/event-crud`
- `feature/event-discovery`
- `feature/registration`
- `feature/recommendations`
- `feature/signalr`
- `feature/ui-polish`

## Rule 2 — Keep commits small

Good:

```text
feat: add Event entity
feat: add event creation form
fix: prevent duplicate registrations
test: add registration validation tests
docs: update setup instructions
```

Avoid:

```text
changes
final
stuff
update
```

## Rule 3 — Pull before starting work

```bash
git checkout main
git pull
git checkout your-branch
git merge main
```

If you have uncommitted work, commit or stash it first.

## Rule 4 — Pull request checklist

Before opening a PR:

- [ ] App builds
- [ ] Feature works locally
- [ ] No passwords/secrets committed
- [ ] No database files committed
- [ ] No `bin/` or `obj/`
- [ ] Code is limited to the task
- [ ] Commit messages are meaningful

## Rule 5 — Never change another person's feature without telling them

Because 11 people are sharing one repository, coordinate changes to shared files such as:

- `Program.cs`
- `Eventify.csproj`
- `MainLayout.razor`
- `app.css`
- `EventifyDbContext.cs`

If two people need the same file, agree on who owns the change and merge deliberately.

## Recommended branch ownership

### Coordinator / frontend
- shared layout
- navigation
- page composition
- UI consistency
- integration

### Backend
- services
- validation
- authentication/authorization
- SignalR server logic

### Database
- entities
- relationships
- EF Core configuration
- migrations
- seed data

### Feature developers
- event CRUD
- registration
- recommendations
- organizer workflow

### QA
- tests
- bug list
- regression testing

### Documentation / DevOps
- README
- API/architecture documentation
- deployment
- final setup verification
