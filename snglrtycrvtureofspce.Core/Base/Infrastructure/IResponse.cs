namespace snglrtycrvtureofspce.Core.Base.Infrastructure;

/// <summary>
/// Represents a standard response interface.
/// </summary>
public interface IResponse
{
    /// <summary>Optional response message.</summary>
    string? Message { get; set; }
}