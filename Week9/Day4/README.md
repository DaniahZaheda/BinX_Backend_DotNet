# Day 4 — Deployment & Mentor Code Review

## Overview

Deployed the Task & Project Management API to Railway and configured the production environment.

## Work Completed

* Connected the project to **Railway** through GitHub.
* Configured the API service using a **Dockerfile**.
* Set the API root directory and `main` branch.
* Added production environment variables for:

  * SQL Server
  * Redis
  * JWT
* Created a **Microsoft SQL Server** service with a persistent volume.
* Connected the API and SQL Server using Railway private networking.
* Verified that **GitHub Actions CI** builds and tests the project successfully.

## Deployment Issue

The SQL Server volume is limited to **500 MB** and reached **98% usage**, causing the SQL Server service to crash with a `No space left on device` error.

The issue was identified as a storage limitation in the Railway environment.

## Status

* Railway setup: ✅
* Docker deployment: ✅
* Production configuration: ✅
* CI pipeline: ✅
* SQL Server: 
* Final deployment verification
