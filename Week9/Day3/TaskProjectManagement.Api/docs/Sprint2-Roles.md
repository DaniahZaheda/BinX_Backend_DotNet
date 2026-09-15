# Sprint 2 — Role Structure

## Roles

### Admin

The Admin has full access to the system and can manage users, roles, projects, and other administrative operations.

### User

The User can create and manage projects they own and access project-related features according to their permissions.

## Authorization Plan

| Endpoint | User | Admin |
|----------|------|-------|
| GET /api/projects | Allowed | Allowed |
| POST /api/projects | Allowed | Allowed |
| Update Project | Owner only | Allowed |
| Delete Project | Owner only | Allowed |
| Manage Users | Not Allowed | Allowed |
| Manage Roles | Not Allowed | Allowed |

## Sprint 2 Goal

Implement real authentication and authorization using ASP.NET Core Identity and role-based access control.