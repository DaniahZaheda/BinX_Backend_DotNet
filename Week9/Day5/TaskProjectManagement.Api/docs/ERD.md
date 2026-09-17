# Task & Project Management API — ERD

```mermaid
erDiagram

    APPLICATION_USER ||--o{ PROJECT : owns
    APPLICATION_USER ||--o{ PROJECT_MEMBER : joins
    PROJECT ||--o{ PROJECT_MEMBER : has

    PROJECT ||--o{ TASK_ITEM : contains
    APPLICATION_USER o|--o{ TASK_ITEM : assigned_to

    TASK_ITEM ||--o{ COMMENT : has
    APPLICATION_USER ||--o{ COMMENT : writes

    APPLICATION_USER {
        string Id PK
        string Email
        string FirstName
        string LastName
    }

    PROJECT {
        int Id PK
        string Name
        string Description
        string OwnerId FK
        datetime CreatedAt
        datetime UpdatedAt
    }

    PROJECT_MEMBER {
        int ProjectId PK_FK
        string UserId PK_FK
        string Role
        datetime JoinedAt
    }

    TASK_ITEM {
        int Id PK
        int ProjectId FK
        string AssignedToId FK
        string Title
        string Description
        int Status
        int Priority
        datetime DueDate
        datetime CreatedAt
        datetime UpdatedAt
    }

    COMMENT {
        int Id PK
        int TaskItemId FK
        string UserId FK
        string Content
        datetime CreatedAt
    }