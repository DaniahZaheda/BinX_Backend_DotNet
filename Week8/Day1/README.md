**# Day 1 — Sprint 3 Planning & N+1 Diagnosis**

**## Sprint 3 Goal**

Improve API performance by identifying and reducing unnecessary database queries, with a focus on detecting N+1 query problems in the main list endpoints.

**## EF Core Query Logging**

Enabled EF Core SQL query logging in development using `LogTo()` and `EnableSensitiveDataLogging()` to inspect the actual SQL queries executed by the API.

**## N+1 Diagnosis**

Tested the `GET /api/projects` endpoint and inspected the generated SQL queries.

The endpoint executed the project query directly with pagination and DTO projection. No additional query was generated for each project, so **no N+1 problem was detected in this endpoint**.

This establishes a baseline for comparing query behavior as more related-data endpoints are added.


## N+1 Query Diagnosis

### Endpoint Tested

```text
GET /api/projects
```

The endpoint was tested with EF Core SQL query logging enabled.

The request executed:

1. One `COUNT` query to retrieve the total number of projects.
2. One `SELECT` query to retrieve the paginated project list.

The generated SQL did not show repeated queries for individual projects or related entities.

### Result

No genuine N+1 query problem was identified in the current `ProjectsController`.

The endpoint uses projection with `Select()` and retrieves only the required project fields in a single query.

### Backlog Task

Although no N+1 issue was found in the current endpoints, the project contains related entities such as `Tasks`, `Comments`, `Members`, `Owner`, and `AssignedTo`.

**Backlog:** Review future endpoints that load these related entities to ensure they use appropriate projection or eager loading and do not introduce N+1 queries.

### Conclusion

The current project list endpoint is not affected by an N+1 query problem based on the SQL logs collected during Sprint 3 Day 1.


## SQL Query Logging

EF Core SQL logging was enabled in `Program.cs` using:

```csharp
.LogTo(Console.WriteLine, LogLevel.Information)
.EnableSensitiveDataLogging();
```

This allowed the generated SQL queries to be monitored directly from the application console.

## Endpoint Testing

The following endpoint was tested:

```text
GET /api/projects
```

The endpoint returned HTTP 200 successfully.

The SQL logs showed:

* One `COUNT` query for the total number of projects.
* One `SELECT` query for the paginated project data.
* No repeated queries for individual records.
* No N+1 query pattern was detected.

## Sprint 3 Day 1 Findings

The current project list endpoint has a clean query pattern and does not demonstrate an N+1 problem.

Future endpoints that load related entities such as Tasks, Comments, Members, Owner, or AssignedTo should be monitored to prevent N+1 queries.

## Day 1 Status

* [x] Sprint 3 planning
* [x] EF Core SQL logging enabled
* [x] 50+ test projects seeded
* [x] List endpoint tested
* [x] Count query verified
* [x] SQL queries reviewed
* [x] N+1 diagnosis completed
* [x] Backlog optimization documented
