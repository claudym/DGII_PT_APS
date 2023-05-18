using System.Net;
using System.Text.Json;
using DGIIAPP.Application.DTOs;

namespace DGIIAPP.API.Middlewares;
/// <summary>
/// Exception Middleware.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class. 
    /// </summary>
    /// <param name="next">Next delegate to be executed.</param>
    public ExceptionMiddleware(RequestDelegate next) {
        _next = next;
    }

    /// <summary>
    /// Invokes the handler implementations.
    /// </summary>
    /// <param name="httpContext">Current http context.</param>
    /// <returns>Asynchronous task.</returns>
    public async Task InvokeAsync(HttpContext httpContext) {
        try {
            await _next(httpContext);
        }
        catch (Exception ex) {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
        string responseMessage = $"Unknown error occurred.";
        var statusCode = HttpStatusCode.InternalServerError;

        try {
            if (exception is NotFoundException) {
                statusCode = HttpStatusCode.NotFound;
            } else if (exception is HttpRequestException) {
                statusCode = HttpStatusCode.ServiceUnavailable;
                if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 500)
                    statusCode = (HttpStatusCode)context.Response.StatusCode;
            }
            responseMessage = exception.Message;
        }
        finally {
            var result = ResultDTO.Invalid(responseMessage);
            var json = JsonSerializer.Serialize(result);
            await SetContext(context, statusCode, json);
        }
    }

    private Task SetContext(HttpContext context, HttpStatusCode code, string json) {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(json);
    }
}
