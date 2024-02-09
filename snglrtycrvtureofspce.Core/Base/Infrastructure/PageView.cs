namespace snglrtycrvtureofspce.Core.Base.Infrastructure;

/// <summary>The page view</summary>
/// <typeparam name="TModel"></typeparam>
public class PageView<TModel> where TModel : class
{
    /// <summary>Current page</summary>
    public int Page { get; set; }

    /// <summary>Count per page</summary>
    public int Count { get; set; }

    /// <summary>Total item</summary>
    public int Total { get; set; }

    /// <summary>List of elements</summary>
    public IList<TModel> Elements { get; set; }
}