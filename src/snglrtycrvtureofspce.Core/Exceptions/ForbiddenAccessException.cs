using System;

namespace snglrtycrvtureofspce.Core.Exceptions;

public class ForbiddenAccessException(string message = "Access denied") : Exception(message);