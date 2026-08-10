# Day 2 — JWT Authentication & Token Issuance


In this task, I implemented JWT-based authentication in an ASP.NET Core Web API using ASP.NET Core Identity.

The API allows users to register, log in using their credentials, and receive a signed JWT token after successful authentication.

## What I Learned

* Understanding the structure of a JWT and its claims.
* Creating a login endpoint using ASP.NET Core Identity.
* Verifying user credentials using `SignInManager`.
* Creating and signing JWT tokens.
* Configuring JWT Bearer Authentication.
* Protecting API endpoints using `[Authorize]`.
* Setting token expiration.
* Testing authentication and JWT tokens using Postman.

## Implementation

### Register

Users can create an account using:

```text
POST /api/Auth/register
```

### Login

Registered users can log in using:

```text
POST /api/Auth/login
```

If the credentials are valid, the API returns a signed JWT containing the user's ID and email.

The token expires after 15 minutes.

### JWT Authentication

JWT Bearer Authentication was configured in `Program.cs` to validate:

* Issuer
* Audience
* Token expiration
* Signing key






