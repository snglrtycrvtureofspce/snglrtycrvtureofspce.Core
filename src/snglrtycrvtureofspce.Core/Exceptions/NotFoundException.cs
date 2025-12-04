using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace snglrtycrvtureofspce.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(IEnumerable<ValidationFailure> errors)
        : base(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)))
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NotFoundException(params string[] messages) : base(string.Join(Environment.NewLine, messages))
    {
    }
}
