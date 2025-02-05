using System;

namespace snglrtycrvtureofspce.Core.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(string message = "Access denied") : base(message) { }
}