# Week 6 — Task & Project Management API

## Overview

This week, I worked on building the core functionality of a Task & Project Management API using ASP.NET Core, Entity Framework Core, SQL Server, and Postman.

## Day 1 — Project Setup

- Set up the ASP.NET Core Web API project.
- Configured EF Core with SQL Server.
- Configured ASP.NET Core Identity.
- Created the initial data model and migration.

## Day 2 — EF Core Model & Migrations

- Implemented the main entity classes.
- Configured relationships using Fluent API.
- Added primary and foreign keys.
- Configured delete behaviors.
- Added seed data.
- Created and applied EF Core migrations.

## Day 3 — Read Operations

- Implemented `GET /api/projects`.
- Added pagination using `page` and `pageSize`.
- Added search and owner filtering.
- Added sorting options.
- Created DTOs for API responses.
- Tested the endpoint using Postman.

![Day 3 Pagination](Day3/screenshots/day3-pagination.png)

## Day 4 — Write Operations & Business Logic

- Implemented `POST /api/projects`.
- Added `ProjectService`.
- Added duplicate project validation.
- Added automatic project member creation.
- Implemented database transactions.
- Tested successful and invalid requests using Postman.

![Create Project](Day4/screenshots/day4-create-project.png)

![Duplicate Project](Day4/screenshots/day4-duplicate-project.png)

## Day 5 — Sprint Review & Retrospective

- Reviewed the completed Sprint 1 features.
- Demonstrated the API using Postman.
- Reviewed the Sprint acceptance criteria.
- Identified improvements for the next sprint.
- Created a Sprint 2 backlog.
- Defined one concrete action for Sprint 2.

### What Went Well

- Successfully implemented the main API functionality.
- Improved my understanding of EF Core and database relationships.
- Implemented real business logic and transactions.

### What Could Be Improved

- Automated testing should be introduced earlier.
- More time should be allocated for reviewing migrations and business logic.

### Sprint 2 Action

Write automated tests for the main business logic before implementing additional API features.

## Technologies

- C#
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- LINQ
- Postman
- Git & GitHub