using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using snglrtycrvtureofspce.Core.Exceptions;

namespace snglrtycrvtureofspce.Core.Middlewares;

/// <summary>
/// Middleware that provides centralized exception handling for ASP.NET Core applications.
/// Converts exceptions to appropriate HTTP responses with consistent error format.
/// </summary>
/// <remarks>
/// This middleware catches exceptions thrown by downstream middleware and converts them
/// to appropriate HTTP responses. It supports the following exception types:
/// <list type="bullet">
///     <item><description><see cref="ValidationException"/> - Returns HTTP 400 Bad Request</description></item>
///     <item><description><see cref="NotFoundException"/> - Returns HTTP 404 Not Found</description></item>
///     <item><description><see cref="UnauthorizedAccessException"/> - Returns HTTP 401 Unauthorized</description></item>
///     <item><description><see cref="ForbiddenAccessException"/> - Returns HTTP 403 Forbidden</description></item>
///     <item><description><see cref="TimeoutException"/> - Returns HTTP 408 Request Timeout</description></item>
///     <item><description><see cref="ConflictException"/> - Returns HTTP 409 Conflict</description></item>
///     <item><description><see cref="NotImplementedException"/> - Returns HTTP 501 Not Implemented</description></item>
///     <item><description><see cref="DbUpdateException"/> - Returns HTTP 500 Internal Server Error</description></item>
/// </list>
///
/// Register this middleware in your request pipeline:
/// <code>
/// app.UseMiddleware&lt;ExceptionHandlingMiddleware&gt;();
/// </code>
/// </remarks>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    /// <summary>
    /// Invokes the middleware, catching and handling any exceptions that occur.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, "Validation error.", ex.Errors);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status401Unauthorized, "Unauthorized.", ex.Message);
        }
        catch (ForbiddenAccessException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status403Forbidden, "Forbidden.", ex.Message);
        }
        catch (TimeoutException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status408RequestTimeout, "Request timed out.", ex.Message);
        }
        catch (DbUpdateException ex)
            when (IsForeignKeyViolationExceptionMiddleware.CheckForeignKeyViolation(ex, out var referencedObject))
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "Unable to delete object. It is referenced by",
                referencedObject);
        }
        catch (DbUpdateException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "Database error.",
                ex.InnerException?.Message ?? ex.Message);
        }
        catch (OperationCanceledException)
        {
            context.Response.StatusCode = 499;
        }
        catch (ConflictException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status409Conflict, "Conflict.", ex.Message);
        }
        catch (NotImplementedException)
        {
            await HandleExceptionAsync(context, StatusCodes.Status501NotImplemented, "Not implemented.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred.");
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        int statusCode,
        string message,
        object? details = null)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = statusCode;

        var response = new { message, details };
        return context.Response.WriteAsJsonAsync(response);
    }
}
