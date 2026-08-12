# Day 4 — Input Validation with FluentValidation

## What I Learned

* Using FluentValidation for input validation.
* Creating validators with custom rules and messages.
* Integrating validation into the ASP.NET Core pipeline.
* Testing validation errors using Postman.

## What I Implemented

* Added FluentValidation.
* Created `RegisterValidator` and `UpdateUserValidator`.
* Added automatic `400 Bad Request` validation.
* Tested valid and invalid requests in Postman.

## Screenshots

### Register Validation

![Register Validation](../screenshots/register-empty-email.png)

### Invalid Email

![Invalid Email](../screenshots/register-invalid-email.png)

### Short Password

![Short Password](../screenshots/register-short-password.png)

### Update Validation

![Update Validation](../screenshots/update-empty-email.png)

### Valid Request

![Valid Request](../screenshots/valid-request.png)

## Technologies

C# • ASP.NET Core Web API • FluentValidation • Postman
