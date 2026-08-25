# Database Schema

## Entities

### ApplicationUser

ASP.NET Core Identity user.

Main fields:

- Id
- UserName
- Email
- PasswordHash
- FirstName
- LastName

### Project

Represents a project.

Main fields:

- Id
- Name
- Description
- OwnerId
- CreatedAt
- UpdatedAt

### ProjectMember

Represents project membership.

Main fields:

- ProjectId
- UserId
- JoinedAt
- Role

### TaskItem

Represents a task inside a project.

Main fields:

- Id
- ProjectId
- AssignedToId
- Title
- Description
- Status
- Priority
- DueDate
- CreatedAt
- UpdatedAt

### Comment

Represents a comment on a task.

Main fields:

- Id
- TaskItemId
- UserId
- Content
- CreatedAt

## Relationships

- One user can own many projects.
- One project can contain many tasks.
- Users and projects have a many-to-many relationship through ProjectMember.
- One task can contain many comments.
- One user can create many comments.
- A task can optionally be assigned to a user.