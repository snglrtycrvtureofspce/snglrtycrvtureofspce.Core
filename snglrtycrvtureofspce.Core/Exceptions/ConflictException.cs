﻿using System;

namespace snglrtycrvtureofspce.Core.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}