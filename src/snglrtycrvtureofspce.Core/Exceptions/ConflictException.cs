namespace snglrtycrvtureofspce.Core.Exceptions;

/// <summary>
/// Exception thrown when a conflict occurs during resource modification.
/// This exception is typically handled by middleware to return HTTP 409 status code.
/// </summary>
/// <remarks>
/// Use this exception when there is a conflict with the current state of the resource,
/// such as duplicate entries, concurrent modification conflicts, or business rule violations.
/// </remarks>
/// <example>
/// <code>
/// if (await repository.ExistsAsync(email))
///     throw new ConflictException($"User with email {email} already exists");
/// </code>
/// </example>
public class ConflictException(string message) : Exception(message);
