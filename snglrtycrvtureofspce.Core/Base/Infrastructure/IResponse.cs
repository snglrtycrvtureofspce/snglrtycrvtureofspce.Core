namespace snglrtycrvtureofspce.Core.Base.Infrastructure;

/// <summary>Interface of response</summary>
public interface IResponse
{
    /// <summary>Return Message</summary>
    string? Message { get; set; }

    /// <summary>Status code</summary>
    int StatusCode { get; set; }
}