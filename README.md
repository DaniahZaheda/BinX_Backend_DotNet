# BinXTech Backend .NET Internship

This repository contains my notes, exercises, assignments, and practical projects completed during the **BinXTech Backend .NET Internship**.

The repository is organized by weeks and days to document my learning progress and practical development experience.

## Internship Information

* **Track:** Backend Development
* **Technology:** .NET and C#
* **Training Provider:** BinXTech
* **Trainee:** Daniah Mohammed Zaheda

## Main Capstone Project

### Task & Project Management API

A backend REST API for managing projects, tasks, users, and comments.

The project was developed as the main practical project during the internship and applies the backend concepts learned throughout the training.

### Main Features

* User registration and login
* JWT authentication
* Role-based authorization
* Project management
* Project ownership
* Project members
* Task management
* Comments
* Pagination
* Search
* Filtering
* Sorting
* Entity Framework Core
* SQL Server
* Redis caching
* API testing
* Automated CI using GitHub Actions
* Docker-based deployment setup

## Technologies

* C#
* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* JWT
* Redis
* Docker
* xUnit
* GitHub Actions
* Swagger / OpenAPI
* Git & GitHub

## Testing

The project includes automated tests using **xUnit**.

The latest test run completed successfully:

* **Total tests:** 9
* **Passed:** 9
* **Failed:** 0

The repository also includes a GitHub Actions workflow for restoring, building, updating the test database, and running the tests.

## API Documentation

The API is documented using **Swagger / OpenAPI**.

The main API areas include:

* Authentication
* Projects
* Project pagination, search, filtering, and sorting
* Project owner information

## Repository Structure

```text
BinX_Backend_DotNet/
│
├── Week1/
├── Week2/
├── Week3/
├── Week4/
├── Week5/
├── Week6/
├── Week7/
├── Week8/
├── Week9/
│   └── Day3/
│       └── TaskProjectManagement.Api/
│
├── .github/
│   └── workflows/
│       └── ci.yml
│
└── README.md
```

Each week contains:

* Weekly learning materials
* Practical assignments
* Source code
* Daily README files
* Screenshots and documentation when needed

## How to Run the Capstone Project

### 1. Clone the repository

```bash
git clone https://github.com/DaniahZaheda/BinX_Backend_DotNet.git
```

### 2. Open the project

```bash
cd BinX_Backend_DotNet/Week9/Day3/TaskProjectManagement.Api
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Apply database migrations

```bash
dotnet ef database update
```

### 5. Run the API

```bash
dotnet run
```

After running the project, Swagger can be used to explore and test the API endpoints.

## Learning Objectives

During the internship, I developed practical experience in:

* C# programming
* Object-Oriented Programming
* LINQ
* Collections and exception handling
* ASP.NET Core
* REST API development
* Authentication and Authorization
* Entity Framework Core
* SQL Server
* Automated testing
* Git and GitHub
* CI/CD concepts
* Docker
* Backend project development

## Internship Progress

The repository documents the development process from the early C# and .NET exercises to the final backend capstone project.

Each week builds on the previous topics and includes practical implementation, testing, debugging, and documentation.

## Author

**Daniah Mohammed Zaheda**

Backend Development Trainee — BinXTech
