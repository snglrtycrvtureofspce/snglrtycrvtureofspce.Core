using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using snglrtycrvtureofspce.Core.Exceptions;

namespace snglrtycrvtureofspce.Core.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
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
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError,
                "Unable to delete object. It is referenced by", referencedObject);
        }
        catch (DbUpdateException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Database error.",
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

    private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message,
        object? details = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new { message, details };
        return context.Response.WriteAsJsonAsync(response);
    }
}