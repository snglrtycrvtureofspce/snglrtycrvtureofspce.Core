using System;

namespace snglrtycrvtureofspce.Core.Exceptions;

public class ConflictException(string message) : Exception(message);