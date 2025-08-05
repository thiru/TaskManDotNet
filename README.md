# TaskMan

A simple website to manage your tasks.

## Pre-requisites

- .NET Core 9+
- ASP.NET Runtime

## Develop

To simply start the app run:

```shell
dotnet run
```

A web server will be started at [http://localhost:8000](http://localhost:8000).

## Tech Stack

### Backend

- Language: C#
- Web server: ASP.NET
- Database: EntityFramework Core

### Frontend

- Language: Javascript
- Framework: [htmx](https://htmx.org/)
- UI: [Bulma](https://bulma.io/documentation/)

## Considerations

The following describes some of the major limitations, assumptions and trade-offs with the current implementation.

- Single user environment
  - no user management exists
- Only one flat category of tasks
  - i.e. no grouping or nesting
- Number of tasks is relatively small so no pagination is necessary
- Without pagination searching should also not be required
  - built-in browser search should suffice
- Tasks are written in plain text
  - perhaps supporting markdown would be better
- No undo capability
  - Deleting and editing tasks are permanent and irrevocable
  - Instead of hard-deleting tasks from the DB we could instead consider a soft delete (flag column, etc.)
- For prod, include middleware to handle exception and show friendly message to user
- Replace console logs with a proper logging system
- No unit tests, etc. have been written
