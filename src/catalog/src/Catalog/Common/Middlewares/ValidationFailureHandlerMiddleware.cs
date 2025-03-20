namespace Catalog.Common.Middlewares;

public class ValidationFailureHandlerMiddleware : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CatalogValidationException e)
        {
            var badRequest = TypedResults.BadRequest(e);
            await badRequest.ExecuteAsync(context);
        }
    }
}

public static class ValidationFailureHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseValidationFailureHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidationFailureHandlerMiddleware>();
    }
}