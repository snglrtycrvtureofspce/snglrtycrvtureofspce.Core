using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace snglrtycrvtureofspce.Core.Contracts.Errors;

public interface IValidationErrorProvider
{
    /// <summary>
    /// Returns a validation error indicating that an entity was not found.
    /// </summary>
    IEnumerable<ValidationFailure> NotFound();

    /// <summary>
    /// Returns a validation error indicating that an entity was not found by its Id.
    /// </summary>
    /// <param name="id">The Id of the entity.</param>
    IEnumerable<ValidationFailure> NotFound(Guid id);
}
