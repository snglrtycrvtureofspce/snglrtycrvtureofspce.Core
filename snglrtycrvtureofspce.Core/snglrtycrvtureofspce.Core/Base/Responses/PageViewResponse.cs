using snglrtycrvtureofspce.Core.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Base.Responses;

public class PageViewResponse<T> : PageView<T>, IResponse where T : class
{
    /// <summary>Message</summary>
    public string Message { get; set; }

    /// <summary>Status code    ///</summary>
    public int StatusCode { get; set; }
}