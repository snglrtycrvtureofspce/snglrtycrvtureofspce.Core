using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace snglrtycrvtureofspce.Core.Errors;

public static class UserErrors
{
    public static IEnumerable<ValidationFailure> NotFound(Guid id) => 
        new List<ValidationFailure> { new(nameof(id), $"User not found. Id: {id}") };
}