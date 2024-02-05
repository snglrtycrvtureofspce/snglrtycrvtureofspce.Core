namespace snglrtycrvtureofspce.Core.Infrastructure;

/// <summary>
/// Standard response of Create, Update, Get single operation
/// </summary>
/// <typeparam name="T"></typeparam>
public class ItemResponse<T> : IResponse where T : class
{
    /// <summary>Message of operation</summary>
    public string Message { get; set; }

    /// <summary>Status code</summary>
    public int StatusCode { get; set; }

    /// <summary>Item or null</summary>
    public T Item { get; set; }
}