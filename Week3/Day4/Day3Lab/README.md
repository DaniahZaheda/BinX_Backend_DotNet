# Day 4 Lab — Implementing CRUD Operations with EF Core

## Overview

This project implements CRUD operations using **Entity Framework Core** in an ASP.NET Core Web API application.

## Technologies

* ASP.NET Core Web API
* C#
* Entity Framework Core 9
* SQL Server
* Swagger

## Implemented Features

Implemented full CRUD operations for the **Student** entity:

* **Create**: Add a new student using POST request.
* **Read**:

  * Get all students.
  * Get student by ID.
* **Update**: Modify existing student data.
* **Delete**: Remove a student from the database.

## API Endpoints

| Method | Endpoint             | Description       |
| ------ | -------------------- | ----------------- |
| GET    | `/api/students`      | Get all students  |
| GET    | `/api/students/{id}` | Get student by ID |
| POST   | `/api/students`      | Create student    |
| PUT    | `/api/students/{id}` | Update student    |
| DELETE | `/api/students/{id}` | Delete student    |

## EF Core Concepts Used

* Async database operations:

  * `ToListAsync()`
  * `FirstOrDefaultAsync()`
  * `SaveChangesAsync()`

* Entity tracking for update and delete operations.

* Handling missing resources using `NotFound()` response.

## Testing

The API was tested using Swagger UI.

Run the project:

```bash
dotnet run
```

Swagger URL:

```text
http://localhost:5168/swagger
```

## Learning Outcome

Learned how to build REST API CRUD operations using ASP.NET Core and Entity Framework Core with SQL Server.
