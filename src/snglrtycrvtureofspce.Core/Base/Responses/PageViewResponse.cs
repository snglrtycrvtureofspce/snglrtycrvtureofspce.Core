using snglrtycrvtureofspce.Core.Contracts.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Base.Responses;

/// <summary>
/// Standard paginated response including metadata and message.
/// </summary>
/// <typeparam name="T">Type of elements in the page.</typeparam>
public class PageViewResponse<T> : PageView<T>, IResponse where T : class
{
    /// <summary>Optional message attached to the response.</summary>
    public string? Message { get; set; }
}
