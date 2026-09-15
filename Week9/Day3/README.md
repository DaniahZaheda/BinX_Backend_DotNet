[![Build and Test](https://github.com/DaniahZaheda/BinX_Backend_DotNet/actions/workflows/ci.yml/badge.svg)](https://github.com/DaniahZaheda/BinX_Backend_DotNet/actions/workflows/ci.yml)


# Week 9 - Day 3: CI/CD Pipeline with GitHub Actions

[![Build and Test](https://github.com/DaniahZaheda/BinX_Backend_DotNet/actions/workflows/ci.yml/badge.svg)](https://github.com/DaniahZaheda/BinX_Backend_DotNet/actions/workflows/ci.yml)

## Objective

Build a Continuous Integration (CI) pipeline using GitHub Actions to automatically restore, build, and test the project whenever code is pushed or a Pull Request is created.

## What I Did

* Created a GitHub Actions workflow for the project.
* Configured the workflow to run on `push` and `pull_request`.
* Set up the .NET 9 environment.
* Added dependency restore and project build steps.
* Added SQL Server as a service for automated testing.
* Configured a separate test database.
* Added Entity Framework Core database migrations before running tests.
* Configured the test environment to avoid running database migrations multiple times.
* Added the project tests to the CI pipeline.
* Verified that the pipeline fails when there are CI/test problems.
* Fixed the issues and verified that the pipeline passes successfully.
* Added a GitHub Actions status badge to this README.

## Pipeline Steps

The workflow performs the following steps:

1. Checkout the repository.
2. Set up .NET 9.
3. Start SQL Server.
4. Wait for SQL Server to become ready.
5. Restore project dependencies.
6. Build the solution.
7. Install the Entity Framework Core CLI.
8. Apply database migrations to the test database.
9. Run the automated tests.

## Technologies

* GitHub Actions
* .NET 9
* ASP.NET Core
* Entity Framework Core
* SQL Server
* xUnit
* GitHub

## Result

The CI pipeline is working successfully. Every push or pull request automatically builds the project and runs the tests.

**Status: CI Pipeline Passing ✅**
