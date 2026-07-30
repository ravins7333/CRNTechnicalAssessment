# CRN Technical Assessment – RESTful Product API

## Overview

This project is a RESTful Web API developed as part of the **CRN Technosoft Technical Assessment**.

The API is built using **ASP.NET Core 8 Web API** with **Entity Framework Core** and **SQL Server**, following a layered architecture and RESTful design principles.

## Tech Stack

* .NET 8
* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger / OpenAPI
* xUnit (Testing)

## Project Structure

```text
CRNTechnicalAssessment
│
├── CRN.API
├── CRN.Application
├── CRN.Domain
├── CRN.Infrastructure
├── CRN.API.Tests
├── CRN.Application.Tests
└── CRNTechnicalAssessment.sln
```

## Features

* User Registration
* User Login (JWT Authentication)
* Product CRUD Operations
* Repository Pattern
* Unit of Work Pattern
* Entity Framework Core
* SQL Server Database
* Swagger API Documentation
* Layered Architecture

## Product API Endpoints

| Method | Endpoint            | Description       |
| ------ | ------------------- | ----------------- |
| GET    | `/api/Product`      | Get All Products  |
| GET    | `/api/Product/{id}` | Get Product By Id |
| POST   | `/api/Product`      | Create Product    |
| PUT    | `/api/Product/{id}` | Update Product    |
| DELETE | `/api/Product/{id}` | Delete Product    |

## Authentication

JWT Token based authentication has been implemented.

Authentication endpoints:

* Register User
* Login User

## Running the Project

1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Update the SQL Server connection string in `appsettings.json`.
4. Run Entity Framework migrations.
5. Start the API.
6. Open Swagger and test the endpoints.

## Database

Database: SQL Server

Entity Framework Core Migrations are included in the project.

## Testing

The solution contains test projects for API and Application layers.

## Author

**Ravin Singh**
