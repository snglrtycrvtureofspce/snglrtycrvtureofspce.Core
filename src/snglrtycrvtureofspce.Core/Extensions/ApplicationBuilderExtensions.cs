using Microsoft.AspNetCore.Builder;
using snglrtycrvtureofspce.Core.Middlewares;

namespace snglrtycrvtureofspce.Core.Extensions;

/// <summary>
/// Extension methods for configuring the application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the core exception handling middleware to the pipeline.
    /// This middleware catches exceptions and returns appropriate HTTP responses.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    /// <example>
    /// <code>
    /// app.UseCoreMiddlewares();
    /// </code>
    /// </example>
    public static IApplicationBuilder UseCoreMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        return app;
    }

    /// <summary>
    /// Adds the exception handling middleware to the pipeline.
    /// This middleware catches exceptions and returns appropriate HTTP responses.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
