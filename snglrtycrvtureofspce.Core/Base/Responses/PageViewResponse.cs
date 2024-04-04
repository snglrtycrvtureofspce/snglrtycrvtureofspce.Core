using snglrtycrvtureofspce.Core.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Base.Responses;

/// <summary>Standard Page view response for get List item</summary>
/// <typeparam name="T"></typeparam>
public class PageViewResponse<T> : PageView<T>, IResponse where T : class
{
    /// <summary>Message</summary>
    public string? Message { get; set; }

    /// <summary>Status code    ///</summary>
    public int StatusCode { get; set; }
}