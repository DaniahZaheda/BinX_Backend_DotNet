# Week 6 — Task & Project Management API

## Overview

This week focused on building the core functionality of a Task & Project Management REST API using ASP.NET Core, Entity Framework Core, and SQL Server.

The project was developed incrementally throughout the sprint, starting with the database and data model, followed by read and write operations, business logic, transaction handling, API testing, and finally the Sprint Review and Retrospective.

---

# Day 1 — Project Setup & Initial Data Model

## What I Did

Set up the Task & Project Management API and prepared the initial Entity Framework Core data model.

### Features Implemented

- Created the ASP.NET Core Web API project.
- Configured Entity Framework Core with SQL Server.
- Added ASP.NET Core Identity.
- Created the `ApplicationUser` entity.
- Created the main project management entities.
- Created `ApplicationDbContext`.
- Configured the database connection.
- Added the initial EF Core migration.
- Prepared the project structure for the following development days.

## Technologies

- C#
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity

## Result

The initial API structure and database model were successfully created and prepared for further development.

---

# Day 2 — EF Core Data Model & Migrations

## What I Did

Implemented the full EF Core data model and configured relationships between the project management entities using the Fluent API.

### Features Implemented

- Configured the `Project` entity.
- Configured the `ProjectMember` entity.
- Configured the `TaskItem` entity.
- Configured the `Comment` entity.
- Added primary and foreign keys.
- Configured required fields and maximum lengths.
- Configured relationships using the Fluent API.
- Configured cascade, restrict, and set-null delete behaviors.
- Added initial seed data.
- Created and reviewed EF Core migrations.
- Applied the database migration.

## Technologies

- C#
- .NET 9
- Entity Framework Core
- SQL Server

## Result

The database schema and entity relationships were configured successfully according to the project requirements.

---

# Day 3 — Core Routes: Catalog & Read Operations

## What I Did

Implemented the core read operations for the Task & Project Management API.

### Features Implemented

- Implemented a paginated `GET /api/projects` endpoint.
- Added pagination using `page` and `pageSize`.
- Added filtering by project name or description using `search`.
- Added filtering by project owner using `ownerId`.
- Added sorting by name, descending name, newest, and oldest.
- Created response DTOs instead of exposing EF Core entities directly.
- Used LINQ with `Where`, `OrderBy`, `Skip`, `Take`, and `Select`.
- Used asynchronous EF Core operations with `CountAsync` and `ToListAsync`.
- Tested the endpoint with different query parameter combinations in Postman.

## Endpoint

```http
GET /api/projects