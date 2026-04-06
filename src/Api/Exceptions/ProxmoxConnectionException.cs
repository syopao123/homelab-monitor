namespace Api.Exceptions;

public class ProxmoxConnectionException : Exception
{
    public ProxmoxConnectionException(string message)
        : base(message) { }

    // Good for debugging: pass the inner exception if the HttpClient crashed
    public ProxmoxConnectionException(string message, Exception innerException)
        : base(message, innerException) { }
}
