using snglrtycrvtureofspce.Core.Contracts.Base.Infrastructure;

namespace snglrtycrvtureofspce.Core.Base.Responses;

/// <summary>Standard response of exist operation</summary>
public class ExistResponse : IResponse
{
    /// <summary>Return Message</summary>
    public string Message { get; set; }

    /// <summary>Condition</summary>
    public bool Exist { get; init; }
}
