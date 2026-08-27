# Day 5 — Sprint Review, Postman Demo & Retrospective

## Sprint 1 Review

Reviewed the completed features of the Task & Project Management API and verified the Sprint 1 implementation through Postman and the project code.

## Sprint 1 Completed Features

- Implemented the EF Core data model and entity relationships.
- Added Fluent API configurations for relationships and delete behaviors.
- Added and applied EF Core migrations.
- Added initial seed data.
- Implemented paginated project retrieval.
- Added project search and owner filtering.
- Added project sorting.
- Implemented DTO projection.
- Implemented project creation.
- Added duplicate project validation.
- Added automatic project member creation for the owner.
- Implemented database transaction handling.
- Tested the main API operations using Postman.

## Postman Demo

The Sprint 1 API features were demonstrated and tested using Postman.

### Project List

![Project List](../Day3/screenshots/day3-pagination.png)

### Search and Filtering

![Search Filter](../Day3/screenshots/day3-search-filter.png)

### Project Creation

![Create Project](../Day4/screenshots/day4-create-project.png)

### Duplicate Project Validation

![Duplicate Project](../Day4/screenshots/day4-duplicate-project.png)

### Second Project Creation

![Second Project](../Day4/screenshots/day4-create-second-project.png)

## Sprint Acceptance Criteria

The completed Sprint 1 work was reviewed against the planned requirements:

- API endpoints return the expected HTTP status codes.
- Pagination, filtering, and sorting work correctly.
- Project creation works successfully.
- Duplicate project names are rejected.
- Business logic is handled through the service layer.
- Database transactions are used for multi-step project creation.
- API operations were tested using Postman.
- The project builds successfully without errors.

## Sprint 2 Backlog

- Add authentication and authorization.
- Add automated unit tests for the business logic.
- Add additional validation.
- Implement CRUD operations for tasks.
- Implement CRUD operations for comments.
- Improve API security and error handling.

## Sprint 1 Retrospective

### What Went Well

- Successfully implemented the main project management functionality.
- Improved understanding of Entity Framework Core relationships and migrations.
- Implemented real business logic instead of simple CRUD operations.
- Used database transactions to maintain data consistency.
- Successfully tested the API using Postman.

### What Could Be Improved

- EF Core migration and seed data issues required additional debugging.
- Automated tests should be introduced earlier during development.
- More time should be allocated for reviewing the implementation before moving to the next feature.

### Action for Sprint 2

Write automated tests for the main business logic before implementing additional API features.

## Sprint 1 Result

Sprint 1 successfully delivered the core functionality of the Task & Project Management API, including data modeling, migrations, read operations, write operations, business logic, transaction handling, and Postman testing.

## Build Verification

The project was successfully built without errors.

Screenshot path:

```text
../Day4/screenshots/day4-build-success.png