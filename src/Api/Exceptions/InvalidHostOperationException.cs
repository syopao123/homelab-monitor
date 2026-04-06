using System;

namespace Api.Exceptions;

public class InvalidHostOperationException : Exception
{
    public InvalidHostOperationException(string message)
        : base(message) { }
}
