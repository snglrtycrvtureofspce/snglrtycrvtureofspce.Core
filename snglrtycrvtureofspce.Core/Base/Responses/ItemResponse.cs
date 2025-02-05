using snglrtycrvtureofspce.Core.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Base.Responses;

/// <summary>
/// Standard response of Create, Update, Get single operation
/// </summary>
/// <typeparam name="T"></typeparam>
public class ItemResponse<T> : IResponse where T : class
{
    /// <summary>Message of operation</summary>
    public string Message { get; set; }

    /// <summary>Item or null</summary>
    public T Item { get; set; }
}