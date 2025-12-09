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

- 🎯 **MediatR Integration** - Request/response pipeline with validation and logging behaviors
- ✅ **FluentValidation** - Built-in request validation pipeline behavior
- 🔐 **JWT Authentication** - Ready-to-use JWT token handling and policies
- 🐇 **RabbitMQ** - Message broker integration for microservices communication
- 📚 **Swagger/OpenAPI** - Pre-configured Swagger setup with versioning support
- ⚠️ **Exception Handling** - Centralized exception middleware with consistent error responses
- 🏗️ **Entity Framework Core** - Common patterns and helpers for EF Core
- 🎲 **Result Pattern** - Functional error handling without exceptions
- 🛡️ **Guard Clauses** - Defensive programming helpers
- 📦 **Value Objects** - Email, Phone, Money, Address, DateRange
- 🧩 **Domain Events** - Event-driven architecture support
- 🔄 **Specification Pattern** - Encapsulated query logic

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

### Quick Start

Register all core services in one line:

```csharp
// In Program.cs
builder.Services.AddCore(typeof(Program).Assembly);

// Add middleware
app.UseCoreMiddlewares();
```

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
// Not Found with factory method
throw NotFoundException.For<User>(userId);

// Conflict with field info
throw ConflictException.ForField<User>("Email", email);

// Forbidden with role check
throw ForbiddenAccessException.ForRole("Admin");

// Bad Request
throw new BadRequestException("INVALID_INPUT", "Invalid input data");
```

### Result Pattern

Use Result<T> for functional error handling:

```csharp
public Result<User> GetUser(int id)
{
    var user = _repository.Find(id);
    if (user is null)
        return Result<User>.Failure(Error.NotFound("User.NotFound", $"User {id} not found"));
    
    return Result<User>.Success(user);
}

// Usage with pattern matching
var result = GetUser(1);
return result.Match(
    onSuccess: user => Ok(user),
    onFailure: error => NotFound(error.Message)
);

// Chaining operations
var result = GetUser(1)
    .Map(user => user.Email)
    .Bind(email => ValidateEmail(email))
    .Tap(email => _logger.Log($"Valid email: {email}"));
```

### Guard Clauses

Defensive programming made easy:

```csharp
public void CreateUser(string name, string email, int age)
{
    Guard.AgainstNullOrEmpty(name, nameof(name));
    Guard.AgainstInvalidEmail(email, nameof(email));
    Guard.AgainstNegative(age, nameof(age));
    Guard.AgainstOutOfRange(age, 0, 150, nameof(age));
    
    // Continue with valid data...
}
```

### Value Objects

Use built-in value objects for common types:

```csharp
// Email
var email = Email.Create("user@example.com");

// Phone Number
var phone = PhoneNumber.Create("+1 (555) 123-4567");

// Money with currency
var price = Money.Create(99.99m, "USD");
var total = price * 3;

// Address
var address = Address.Create(
    street: "123 Main St",
    city: "New York",
    postalCode: "10001",
    country: "USA",
    state: "NY"
);

// Date Range
var range = DateRange.Create(DateTime.Today, DateTime.Today.AddDays(7));
if (range.Contains(DateTime.Now)) { /* ... */ }
```

### Entity Base Classes

Use base classes for your domain entities:

```csharp
// Simple entity with Guid ID
public class User : Entity
{
    public string Name { get; set; }
}

// Auditable entity with timestamps
public class Order : AuditableEntity
{
    public decimal Total { get; set; }
    // CreatedAt, CreatedBy, ModifiedAt, ModifiedBy included
}

// Soft-deletable entity
public class Product : SoftDeletableEntity
{
    public string Name { get; set; }
    // IsDeleted, DeletedAt, DeletedBy included
    // Use Delete() and Restore() methods
}
```

### Specification Pattern

Encapsulate query logic:

```csharp
public class ActiveUsersSpec : Specification<User>
{
    public ActiveUsersSpec()
    {
        AddCriteria(u => u.IsActive);
        AddInclude(u => u.Orders);
        ApplyOrderBy(u => u.LastLoginDate);
    }
}

// Usage
var spec = new ActiveUsersSpec();
var users = await _repository.FindAsync(spec);
```

### Extension Methods

Rich set of utility extensions:

```csharp
// Collections
users.IsNullOrEmpty();
items.Batch(100);  // Split into chunks
list.ForEach(x => Process(x));

// Strings
"HelloWorld".ToSnakeCase();  // "hello_world"
"secret@email.com".Mask(3, 4);  // "sec*********\.com"
text.Truncate(50);

// DateTime
date.StartOfMonth();
date.IsWeekend();
date.AddBusinessDays(5);
date.ToRelativeTime();  // "2 hours ago"
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
