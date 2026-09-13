## Day 1 – Sprint 4 Planning & Test Coverage

### Objective

The goal of Day 1 was to review the existing API test coverage, identify important gaps, and add integration tests for authentication, authorization, business logic, and performance regression scenarios.

### Endpoint Audit

| Endpoint                       | Coverage                               | Priority |
| ------------------------------ | -------------------------------------- | -------- |
| `POST /api/Auth/register`      | Success + duplicate email              | High     |
| `POST /api/Auth/login`         | Valid + invalid password               | High     |
| `GET /api/Projects`            | Functional + performance regression    | Medium   |
| `POST /api/Projects`           | Unauthorized access                    | High     |
| `DELETE /api/Projects/{id}`    | Admin success + regular user forbidden | High     |
| `PUT /api/Projects/{id}`       | Owner update success                   | High     |
| `GET /api/Projects/with-owner` | Not tested                             | Low      |
| `GET /api/Projects/projection` | Not tested                             | Low      |

### Tests Added

The following integration tests were added:

1. Register with valid data.
2. Register with duplicate email.
3. Login with valid credentials.
4. Login with invalid password.
5. Create project without authentication.
6. Delete project as a regular user.
7. Delete project as an Admin.
8. Update project as the owner.
9. Verify that `GET /api/Projects` does not introduce an N+1 query problem.

### Performance Regression

A query-count interceptor was added to monitor SQL `SELECT` queries during integration tests.

The `GET /api/Projects` endpoint was tested to ensure that retrieving a paginated list does not generate a separate query for every project.

This protects against future N+1 query regressions.

### Test Result

The complete test suite passed successfully:

```text
Total: 9
Passed: 9
Failed: 0
Skipped: 0
```

### Sprint 4 Progress

The main authentication, authorization, business logic, and performance regression gaps identified during the endpoint audit were covered with automated integr
