using FluentValidation.Results;

namespace snglrtycrvtureofspce.Core.Exceptions;

/// <summary>
/// Exception thrown when a requested resource is not found.
/// This exception is typically handled by middleware to return HTTP 404 status code.
/// </summary>
/// <remarks>
/// Use this exception when an entity or resource cannot be found in the database or storage.
/// The <see cref="ExceptionHandlingMiddleware"/> will automatically convert this to an HTTP 404 response.
/// </remarks>
/// <example>
/// <code>
/// var entity = await repository.GetByIdAsync(id)
///     ?? throw new NotFoundException($"Entity with id {id} not found");
/// </code>
/// </example>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    public NotFoundException() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public NotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with validation errors.
    /// </summary>
    /// <param name="errors">A collection of validation failures to include in the error message.</param>
    public NotFoundException(IEnumerable<ValidationFailure> errors)
        : base(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with multiple error messages.
    /// </summary>
    /// <param name="messages">An array of error messages to combine.</param>
    public NotFoundException(params string[] messages) : base(string.Join(Environment.NewLine, messages))
    {
    }
}
