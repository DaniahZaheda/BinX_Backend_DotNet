# Day 5 Lab — Testing & Documenting the API with Postman

## Overview

This project focuses on testing and documenting the Student CRUD API created in Day 4 using Postman.

## Technologies

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Postman

## What Was Tested

The following endpoints were tested:

* **GET** `/api/students` – Get all students.
* **GET** `/api/students/{id}` – Get a student by ID.
* **POST** `/api/students` – Create a new student.
* **PUT** `/api/students/{id}` – Update an existing student.
* **DELETE** `/api/students/{id}` – Delete a student.

## Postman Testing

* Created a Postman collection for all API endpoints.
* Tested both successful and error scenarios.
* Added test scripts to verify expected status codes.
* Created a Postman environment with a `baseUrl` variable.
* Used `{{baseUrl}}` in all requests for easier testing.

## Learning Outcome

Learned how to organize API requests in Postman, test success and failure cases, use environments and variables, and document API endpoints for easier testing and collaboration.
