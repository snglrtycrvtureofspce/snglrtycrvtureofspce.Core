# snglrtycrvtureofspce.Core

[![CI](https://github.com/snglrtycrvtureofspce/snglrtycrvtureofspce.Core/workflows/CI/badge.svg)](https://github.com/snglrtycrvtureofspce/snglrtycrvtureofspce.Core/actions?query=workflow%3ACI)
[![NuGet](https://img.shields.io/nuget/v/snglrtycrvtureofspce.Core.svg)](https://www.nuget.org/packages/snglrtycrvtureofspce.Core)
[![NuGet Downloads](https://img.shields.io/nuget/dt/snglrtycrvtureofspce.Core.svg)](https://www.nuget.org/packages/snglrtycrvtureofspce.Core)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**Simple SDK for ASP.NET Core with built-in support for MediatR, FluentValidation, JWT authentication, RabbitMQ, and Swagger configuration.**

[Getting Started](#installing-snglrtycrvtureofspcecore) •
[Features](#features) •
[Documentation](#usage) •
[Contributing](CONTRIBUTING.md)

---

## Features

- 🎯 **MediatR Integration** - Request/response pipeline with validation behaviors
- ✅ **FluentValidation** - Built-in request validation pipeline behavior
- 🔐 **JWT Authentication** - Ready-to-use JWT token handling and policies
- 🐇 **RabbitMQ** - Message broker integration for microservices communication
- 📚 **Swagger/OpenAPI** - Pre-configured Swagger setup with versioning support
- ⚠️ **Exception Handling** - Centralized exception middleware with consistent error responses
- 🏗️ **Entity Framework Core** - Common patterns and helpers for EF Core

## Installing snglrtycrvtureofspce.Core

You should install [snglrtycrvtureofspce.Core with NuGet](https://www.nuget.org/packages/snglrtycrvtureofspce.Core):

```powershell
Install-Package snglrtycrvtureofspce.Core
```

Or via the .NET CLI:

```bash
dotnet add package snglrtycrvtureofspce.Core
```

### Using Contracts-Only Package

To reference only the contracts (interfaces) without the full implementation:

```powershell
Install-Package snglrtycrvtureofspce.Core.Contracts
```

This package is useful when:
- You need interfaces in a separate assembly
- Building shared contract libraries
- Reducing dependencies in client projects

## Supported Frameworks

| Framework | Version |
|-----------|---------|
| .NET | 6.0, 8.0 |
| .NET Standard | 2.0 (Contracts only) |
| .NET Framework | 4.6.2+ (Windows only) |

## Usage

### Exception Handling Middleware

Add centralized exception handling to your application:

```csharp
// In Program.cs or Startup.cs
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

The middleware automatically handles:
- `ValidationException` → HTTP 400
- `NotFoundException` → HTTP 404
- `UnauthorizedAccessException` → HTTP 401
- `ForbiddenAccessException` → HTTP 403
- `ConflictException` → HTTP 409
- `TimeoutException` → HTTP 408

### Request Validation with MediatR

Register the validation behavior:

```csharp
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
```

Create validators for your requests:

```csharp
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
```

### Custom Exceptions

Use the built-in exceptions for consistent error handling:

```csharp
// Not Found
throw new NotFoundException($"User with id {id} not found");

// Conflict
throw new ConflictException($"User with email {email} already exists");

// Forbidden
throw new ForbiddenAccessException("Only administrators can perform this action");
```

### Expression Helpers

Dynamic sorting support for LINQ queries:

```csharp
var sortExpression = ExpressionHelpers.GetSortLambda<User>("LastName");
var sortedUsers = users.AsQueryable().OrderBy(sortExpression);
```

### API Versioning

Configure API versioning with Swagger support:

```csharp
services.AddApiVersioningConfiguration();
services.AddSwaggerConfiguration();
```

## Building from Source

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (recommended)

### Build

```powershell
# Restore and build
./Build.ps1

# Build contracts only
./BuildContracts.ps1

# Push to NuGet (requires API key)
./Push.ps1 -ApiKey "your-api-key"
```

## Project Structure

```
snglrtycrvtureofspce.Core/
├── src/
│   ├── snglrtycrvtureofspce.Core/           # Main library
│   │   ├── Base/                            # Base types and responses
│   │   ├── Enums/                           # Common enumerations
│   │   ├── Errors/                          # Error handling utilities
│   │   ├── Exceptions/                      # Custom exceptions
│   │   ├── Extensions/                      # Extension methods
│   │   ├── Filters/                         # MVC filters and behaviors
│   │   ├── Helpers/                         # Utility helpers
│   │   ├── Microservices/                   # Microservices infrastructure
│   │   ├── Middlewares/                     # ASP.NET Core middlewares
│   │   └── Providers/                       # Test providers
│   └── snglrtycrvtureofspce.Core.Contracts/ # Interfaces and contracts
├── test/
│   └── snglrtycrvtureofspce.Core.Tests/     # Unit tests
├── samples/                                  # Usage examples
└── assets/                                   # Logo and assets
```

## Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

Built with:
- [MediatR](https://github.com/jbogard/MediatR) - Simple mediator implementation
- [FluentValidation](https://github.com/FluentValidation/FluentValidation) - Validation library
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - Swagger/OpenAPI tooling
