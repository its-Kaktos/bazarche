using System.Text.Json;

namespace Catalog.Common.Middlewares;

public class ValidationFailureHandlerMiddleware : IMiddleware
{
    // ReSharper disable once InconsistentNaming
    private const int BAD_REQUEST_STATUS_CODE = 400;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CatalogValidationException e)
        {
            context.Response.StatusCode = BAD_REQUEST_STATUS_CODE;
            context.Response.Headers.Append("content-type", "application/json");

            var errorType = ToUnderscoreCase(e.GetType().Name.Replace("Exception", string.Empty));

            var json = JsonSerializer.Serialize(new FailedResultType
            {
                ErrorType = errorType,
                Message = e.Message,
                TraceIdentifier = context.TraceIdentifier,
                Errors = e.Errors
            });

            await context.Response.WriteAsync(json);
        }
    }

    private static string ToUnderscoreCase(string value) => value.Replace(" ", "_").ToLowerInvariant();
}

public static class ValidationFailureHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseValidationFailureHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidationFailureHandlerMiddleware>();
    }
}