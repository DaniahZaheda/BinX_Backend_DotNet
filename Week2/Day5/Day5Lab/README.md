# Day 5 Lab — Middleware Pipeline & Dependency Injection

## Overview

This project demonstrates the basics of the ASP.NET Core middleware pipeline and dependency injection.

## Technologies

* ASP.NET Core Web API
* C#
* Swagger

## Features

* Created a custom middleware to log each incoming request.
* Registered the middleware in the request pipeline.
* Created a service using an interface and its implementation.
* Registered the service with the Dependency Injection container using **Scoped** lifetime.
* Injected the service into a controller using constructor injection.

## Project Structure

* **Controllers** – API endpoints.
* **Services** – Interface and service implementation.
* **Middleware** – Custom request logging middleware.

## How to Run

```bash
dotnet run
```

Open Swagger:

```text
http://localhost:5269/swagger
```

## Learning Outcome

Learned how middleware processes HTTP requests, how dependency injection works in ASP.NET Core, and how to inject services into controllers using constructor injection.
