# Task Manager API with Mediator and CQRS

## Project Overview
This project demonstrates the implementation of a Task Manager API using the Mediator pattern and Command Query Responsibility Segregation (CQRS) architectural principles. By building a practical, real-world application, we explore how these patterns can improve code organization, maintainability, and scalability.

## Architecture

### Mediator Pattern
The Mediator pattern reduces coupling between components by introducing a mediator object that handles the communication between them. In this implementation, we use the MediatR library to:
- Decouple request senders from request handlers
- Simplify cross-cutting concerns through behaviors (like validation and logging)
- Organize code based on features rather than technical layers

### CQRS (Command Query Responsibility Segregation)
This project strictly separates operations that modify state (Commands) from operations that read state (Queries):
- **Commands**: Create, Update, Delete tasks
- **Queries**: Retrieve task lists or details
- **Benefits**: Better performance optimization, simpler scaling, and cleaner domain models

## Features
- Task creation, updating, and deletion
- Task categorization and prioritization
- Task filtering and searching
- User assignment and management
- Completion status tracking

## Technical Stack
- ASP.NET Core 7.0 Web API
- MediatR for implementing the mediator pattern
- Entity Framework Core for data access
- FluentValidation for request validation
- Swagger/OpenAPI for API documentation

## Project Structure
```
src/
├── MediatorCqrs.API           # API Controllers and configuration
├── MediatorCqrs.Application   # Application layer with CQRS implementation
│   ├── Commands/              # Command handlers
│   ├── Queries/               # Query handlers
│   ├── Behaviors/             # Cross-cutting concerns
│   └── Validators/            # Request validation
├── MediatorCqrs.Domain        # Domain models and logic
└── MediatorCqrs.Infrastructure # Data access and external services
```
