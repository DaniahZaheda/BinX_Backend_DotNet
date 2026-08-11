# Day 3 — Authorization & Role-Based Access Control

## What I Learned

* Protecting API endpoints using `[Authorize]`.
* Implementing `User` and `Admin` roles.
* Understanding `401 Unauthorized` vs `403 Forbidden`.
* Adding roles and permissions as JWT claims.
* Creating and applying a named authorization policy.
* Testing protected routes using Postman.

## Testing

* No Token → `401 Unauthorized`
* User → Protected Route → `200 OK`
* User → Admin Route → `403 Forbidden`
* Admin → Admin Route → `200 OK`
* Admin → Policy Route → `200 OK`
