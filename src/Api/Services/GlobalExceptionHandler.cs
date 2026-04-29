using System;
using Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Services;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var (statusCode, message) = exception switch {
            InvalidNodeOperationException ex => (StatusCodes.Status400BadRequest, ex.Message),
            InvalidHostOperationException ex => (StatusCodes.Status400BadRequest, ex.Message),
            ProxmoxConnectionException ex => (StatusCodes.Status400BadRequest, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(new { Error = message }, cancellationToken);
        return true;
    }
}
