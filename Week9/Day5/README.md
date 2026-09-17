# Task & Project Management API

ASP.NET Core Web API for managing projects, tasks, users, and comments.

## Tech Stack

* .NET 9
* ASP.NET Core
* Entity Framework Core
* SQL Server
* Identity + JWT
* Redis
* Swagger
* xUnit
* GitHub Actions
* Docker / Railway

## Run

```bash
dotnet restore
dotnet build TaskProjectManagement.sln
dotnet run --project TaskProjectManagement.Api
```

Swagger:

```text
http://localhost:5005/swagger
```

## Environment Variables

```text
ConnectionStrings__DefaultConnection
ConnectionStrings__Redis
Jwt__Key
Jwt__Issuer
Jwt__Audience
```

## Database

EF Core migrations are located in:

```text
TaskProjectManagement.Api/Migrations
```

ERD:

```text
TaskProjectManagement.Api/docs/ERD.md
```

## Tests

```bash
dotnet test TaskProjectManagement.sln
```

Current result: **9 passed, 0 failed**

## CI / Deployment

GitHub Actions is used for build and test automation.

The API was configured for Railway deployment using Docker.

Public URL:

```text
https://attractive-elegance-production-8896.up.railway.app
```
