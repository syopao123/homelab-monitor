using System;

namespace Api.Exceptions;

public class InvalidNodeOperationException : Exception
{
    public InvalidNodeOperationException(string message)
        : base(message) { }
}
