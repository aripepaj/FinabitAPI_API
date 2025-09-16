# copilot-instructions.md

## Project Overview
This is a .NET 8 Web API project for managing partners, items, transactions, and related business logic. It uses SQL Server as the backend and relies on stored procedures for most data access.

## Coding Guidelines
- Use C# 12.0 features where appropriate.
- Repository classes should use `GlobalRepository.GetConnection()` for database access.
- Always close SQL connections in a `finally` block or use `using` statements.
- Use strongly-typed models (e.g., `PartnerModel`, `Partner`) for API responses instead of `DataTable` where possible.
- Follow the existing naming conventions for methods and parameters.
- Use dependency injection for services and repositories in controllers.
- All configuration (including connection strings) is loaded from `appsettings.json` and injected at startup.

## API Guidelines
- Controllers should return `ActionResult<T>` and use `Ok(result)` for successful responses.
- Prefer returning lists of models, not raw `DataTable` objects, in API endpoints.
- Validate input parameters and handle exceptions gracefully.

## Special Instructions
- The connection string is initialized at startup using `ProgramInitializer.InitializeGlobalRepository(builder.Configuration);`.
- Do not hardcode connection strings or sensitive data in code.
- Use stored procedures for all database operations.
- When adding new endpoints, follow the pattern in `PartnersController`.
