# Product Service API

A .NET 8 RESTful API for managing products with versioning support, built following Clean Architecture principles and Domain-Driven Design practices.

## 🚀 Features

- CRUD operations for products
- API versioning
- Swagger/OpenAPI documentation
- PostgreSQL database with Entity Framework Core
- Clean Architecture implementation
- Domain-driven design approach
- Structured logging
- Business rule validations

## 🏗 Architecture

The solution follows Clean Architecture with these layers:

- **ProductService.API**: REST API endpoints and configuration
- **ProductService.Application**: Application services and DTOs
- **ProductService.Domain**: Business entities and rules
- **ProductService.Infrastructure**: Data persistence and external services

## 🛠 Tech Stack

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger/OpenAPI
- API Versioning

## 🔧 Prerequisites

- .NET 8 SDK
- PostgreSQL
- Docker (optional)

## ⚙️ Configuration

Database connection string in `appsettings.json`:
