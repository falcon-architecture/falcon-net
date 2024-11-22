using Microsoft.Extensions.Logging;

namespace Falcon.Infrastructure.Abstractions.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "An error occurred: {ErrorMessage}", ex.Message);
        context.Response.ContentType = "text/plain";
        context.Response.StatusCode = 500;
        return context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    }
}
