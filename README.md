# .NET 8 Web API Project

This project is a .NET 8 Web API application designed with REST architectural principles. Below, you'll find an overview of the project's structure and key features.

## Overview

The API is accessible via Swagger at the following link: [Swagger API Documentation](https://localhost:7254/swagger/v1/swagger.json).

## Project Structure

The project is divided into several layers to promote separation of concerns, maintainability, and scalability. Each layer has a specific responsibility:

1. **API Layer**: Handles HTTP requests and responses. This layer is responsible for routing and returning appropriate status codes.
2. **Service Layer**: Contains the business logic of the application. It communicates with the Data Access Layer (DAL) to perform CRUD operations.
3. **Data Access Layer (DAL)**: Interacts with the database. This layer contains repositories for accessing and manipulating data.
4. **DTOs (Data Transfer Objects)**: Used to transfer data between the layers. This ensures that only the necessary data is exposed and transferred.

### Why Use Layers?

Using a layered architecture helps to:
- **Separate concerns**: Each layer has a specific responsibility, making the codebase easier to understand and maintain.
- **Promote reusability**: Business logic can be reused across different parts of the application.
- **Enhance testability**: Each layer can be tested independently, leading to more robust and reliable code.

## AutoMapper

We use AutoMapper to handle object-to-object mapping between layers. This simplifies the conversion process and reduces boilerplate code.

## Dependency Injection (DI)

Dependency Injection is used to manage dependencies between layers. This promotes loose coupling and makes the application more modular and testable.

## Scalability

To ensure scalability, we use asynchronous programming (`async` and `await`). This allows the application to handle more concurrent requests efficiently.

## Database

The project uses SQL as the database with a Code First approach. To create the database, you can use the following commands:

```bash
# Add a migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update
```

## Configuration Files

Configuration files are used to store settings and configurations that can be easily modified without changing the code. This includes database connection strings, API keys, and other settings.

## Error Handling

All errors are handled using an error middleware. Errors are logged, and fatal errors are sent via email. This ensures that issues are detected and addressed promptly.

## Request Logging

Every request to the system is logged for the purpose of rating and analyzing user interactions.

## Clean Code

The project follows clean code principles to ensure readability, maintainability, and quality. This includes meaningful naming, proper structuring, and adhering to coding standards.
