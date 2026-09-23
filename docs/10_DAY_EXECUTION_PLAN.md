# Eventify — 10-Day Execution Plan

The goal is not to build every idea in the proposal. The goal is to produce a stable, demonstrable MVP.

## MVP must work

1. Student/organizer/admin role concept
2. User interests
3. Event creation
4. Event browsing/search/filter
5. Event details
6. Registration
7. My registrations
8. Interest-based recommendations
9. Organizer event management
10. Basic notifications / SignalR if core features are stable
11. Deployment
12. Documentation and demo

## Stretch only

- QR-code attendance
- advanced analytics
- sophisticated recommendation algorithms
- complicated notification preferences
- unnecessary microservices
- Docker if it creates deployment risk

## Day 1 — Project foundation

- Everyone clones the repo.
- Verify .NET 10 installation.
- Run the starter app.
- Agree on branch naming.
- Create GitHub Issues/Project board.
- Confirm final MVP scope.
- Database team reviews entities.
- UI team reviews existing pages.

## Day 2 — Data + authentication

- Implement real EF Core persistence.
- Add User/Interest/Event/Registration/Notification entities.
- Create migrations.
- Add real authentication.
- Add role authorization.

## Day 3 — Event management

- Create event.
- Edit event.
- Delete event.
- View event details.
- Browse/search/filter events.

## Day 4 — Registration

- Register for event.
- Prevent duplicate registration.
- Cancel registration.
- My Registrations.
- Organizer sees registrants.

## Day 5 — Recommendations

Start with a transparent matching algorithm:

```text
matching interests / user interests
```

Sort events by number of matching interests and date.

Do not build machine learning.

## Day 6 — SignalR

Implement the simplest meaningful real-time notification:

```text
Organizer publishes event
        ↓
Server identifies matching interest
        ↓
SignalR sends notification
        ↓
Student sees notification
```

If SignalR threatens the stable MVP, finish the rest first.

## Day 7 — UI and integration

- Responsive layout.
- Loading states.
- Empty states.
- Validation messages.
- Consistent buttons/forms.
- Test all roles.

## Day 8 — QA

Test:

- login/logout
- invalid login
- event creation
- event editing
- event deletion
- event filtering
- duplicate registration
- cancellation
- recommendations
- organizer permissions
- admin permissions
- notifications

Freeze the feature list.

## Day 9 — Deployment

- Deploy the monolithic app.
- Configure production connection string.
- Seed demo data.
- Test the deployed URL from another device/network.
- Fix only deployment/blocking bugs.

## Day 10 — Submission

- Final GitHub cleanup.
- Verify repository is accessible.
- Verify deployment.
- Record one group video with every member visible.
- Each member explains their actual contribution.
- Test the YouTube unlisted link in incognito.
- Submit once as the group leader.

## Golden rule

After Day 7:

> NO NEW FEATURES unless a lecturer requirement is missing.

Fix what exists instead.
