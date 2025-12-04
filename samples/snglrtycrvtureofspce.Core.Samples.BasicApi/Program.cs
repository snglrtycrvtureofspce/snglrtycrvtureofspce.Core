using snglrtycrvtureofspce.Core.Middlewares;

namespace snglrtycrvtureofspce.Core.Samples.BasicApi;

/// <summary>
/// Sample minimal API demonstrating snglrtycrvtureofspce.Core usage.
/// </summary>
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Add exception handling middleware
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Configure Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Sample endpoints
        app.MapGet("/", () => "Hello from snglrtycrvtureofspce.Core!")
            .WithName("GetRoot")
            .WithOpenApi();

        app.MapGet("/error/notfound", () =>
        {
            throw new Exceptions.NotFoundException("Sample resource not found");
        }).WithName("ThrowNotFound");

        app.MapGet("/error/conflict", () =>
        {
            throw new Exceptions.ConflictException("Sample conflict error");
        }).WithName("ThrowConflict");

        app.MapGet("/error/forbidden", () =>
        {
            throw new Exceptions.ForbiddenAccessException();
        }).WithName("ThrowForbidden");

        app.Run();
    }
}
