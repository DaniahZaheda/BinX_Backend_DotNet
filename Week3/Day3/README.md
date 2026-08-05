# Day 3 Lab — Entity Framework Core Setup & Code-First Migrations

## Overview

This project demonstrates setting up **Entity Framework Core** with **SQL Server** using the Code-First approach in an ASP.NET Core Web API.

## Technologies

* ASP.NET Core Web API
* C#
* Entity Framework Core 9
* SQL Server

## What Was Implemented

* Added EF Core SQL Server packages.
* Created entity classes:

  * Student
  * Book
* Created `AppDbContext` to manage database communication.
* Configured SQL Server connection string.
* Created and applied Code-First migration.

## Migration Commands

Create migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply migration:

```bash
dotnet ef database update
```

## Database

Created database:

```
Day3DB
```

Tables:

* Students
* Books

## Run Project

```bash
dotnet run
```

Swagger:

```
http://localhost:5168/swagger
```

## Learning Outcome

Learned how to connect an ASP.NET Core application with SQL Server using Entity Framework Core and generate database tables from C# classes.
