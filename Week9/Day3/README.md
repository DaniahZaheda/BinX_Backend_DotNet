# Week 9 – Day 2
## API Documentation

### Objective

Improve API documentation using Swagger/OpenAPI, add request and response examples, and review the existing Postman collection.

### Swagger / OpenAPI

- Enabled XML documentation.
- Added XML comments to the main endpoints.
- Added realistic request and response examples.
- Added Swagger documentation for authentication and project endpoints.

### Examples Added

- `POST /api/Auth/register` — Request + Response
- `POST /api/Auth/login` — Request + Response
- `POST /api/Projects` — Request + Response

Example:

```json
{
  "name": "Travel Management System",
  "description": "A project for managing travel activities and tasks."
}