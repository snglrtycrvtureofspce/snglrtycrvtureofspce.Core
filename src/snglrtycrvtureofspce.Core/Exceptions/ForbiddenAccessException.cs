namespace snglrtycrvtureofspce.Core.Exceptions;

/// <summary>
/// Exception thrown when the user is authenticated but lacks the required permissions.
/// This exception is typically handled by middleware to return HTTP 403 status code.
/// </summary>
/// <remarks>
/// Use this exception when the user is authenticated but doesn't have the required
/// permissions or roles to perform the requested action. For unauthenticated users,
/// use <see cref="UnauthorizedAccessException"/> instead.
/// </remarks>
/// <example>
/// <code>
/// if (!user.HasRole("Admin"))
///     throw new ForbiddenAccessException("Only administrators can perform this action");
/// </code>
/// </example>
public class ForbiddenAccessException(string message = "Access denied") : Exception(message);
