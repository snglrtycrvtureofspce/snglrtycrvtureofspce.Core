using System.Collections.Generic;

namespace snglrtycrvtureofspce.Core.Base.Responses;

/// <summary>
/// Represents a paginated view of items.
/// </summary>
/// <typeparam name="TModel">The type of items in the page.</typeparam>
public class PageView<TModel> where TModel : class
{
    /// <summary>Current page number (1-based).</summary>
    public int Page { get; set; }

    /// <summary>Number of items per page.</summary>
    public int Count { get; set; }

    /// <summary>Total number of items.</summary>
    public int Total { get; set; }

    /// <summary>List of items for the current page.</summary>
    public IList<TModel> Elements { get; set; }
}