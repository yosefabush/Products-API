# Copilot Instruction File for .NET 8 Web API

## General Guidelines
- Use C# 12 language features where appropriate
- Follow Microsoft's recommended clean architecture patterns
- Implement SOLID principles in all code
- Use async/await for all I/O operations
- Use nullable reference types with appropriate annotations
- Use modern C# features like pattern matching, records, and init-only properties
- Implement proper exception handling with custom exception types when needed
- Use dependency injection throughout the application
- Use meaningful variable and method names that clearly explain their purpose
- Include XML documentation for public API endpoints and models

## Project Structure
- Organize code into logical layers (API, Services, Domain, Infrastructure)
- Keep controllers thin by delegating business logic to services
- Use feature folders or vertical slice architecture where appropriate
- Implement interfaces for all services to ensure loose coupling
- Use DTOs to transfer data between layers, never expose domain models directly

## API Design
- Follow RESTful API design principles
- Use appropriate HTTP methods (GET, POST, PUT, DELETE, PATCH)
- Return appropriate HTTP status codes
- Implement proper route naming conventions (/api/resources/identifier)
- Use API versioning to manage changes
- Implement proper model validation using attributes or FluentValidation
- Use ActionResult<T> for controller methods
- Support JSON and JSON:API formats
- Include pagination for collection endpoints
- Implement proper filtering, sorting, and searching capabilities

## Performance and Security
- Implement proper caching strategies (in-memory, distributed, response caching)
- Use minimal APIs for simple endpoints
- Implement rate limiting for API endpoints
- Use HTTPS for all communications
- Implement proper authentication and authorization (JWT, OAuth, API Keys)
- Apply CORS policies appropriately
- Follow OWASP security best practices
- Protect against common web vulnerabilities (XSS, CSRF, injection attacks)
- Implement proper input sanitization and validation
- Use secret management for sensitive information (User Secrets, Azure Key Vault)

## Testability
- Write unit tests for all business logic
- Write integration tests for API endpoints
- Use mocks for external dependencies
- Ensure code has high test coverage
- Design code to be easily testable
- Use test fixtures and factories for test data

## Data Access
- Use Entity Framework Core with code-first approach
- Implement repository pattern where appropriate
- Use database migrations for schema changes
- Implement proper database indexing
- Use efficient querying with projection and filtering at the database level
- Implement database transactions for multi-step operations
- Use database connection pooling
- Handle database concurrency conflicts
- Implement soft delete where appropriate

## Logging and Monitoring
- Use structured logging with Serilog
- Include correlation IDs for request tracing
- Log appropriate information (not too verbose, not too sparse)
- Implement health checks for all services and dependencies
- Use Application Insights
- Implement metrics collection
- Log all exceptions with appropriate context
- Implement proper diagnostic tools

## Deployment and DevOps
- Use Docker for containerization
- Support Kubernetes deployment
- Implement CI/CD pipelines
- Use environment-specific configuration
- Implement proper application startup and shutdown procedures
- Implement database migration automation

## Documentation
- Generate API documentation using Swagger/OpenAPI
- Include examples in API documentation
- Document architectural decisions
- Provide proper README files
- Include environment setup documentation
- Document infrastructure requirements

## Error Handling
- Return standardized error responses
- Include problem details in error responses (RFC 7807)
- Log exceptions with appropriate severity levels
- Handle expected exceptions gracefully
- Use circuit breakers for external dependencies

## Performance Optimization
- Use asynchronous programming
- Implement response compression
- Do not Use minimal API
- Implement caching at appropriate levels
- Optimize JSON serialization/deserialization
