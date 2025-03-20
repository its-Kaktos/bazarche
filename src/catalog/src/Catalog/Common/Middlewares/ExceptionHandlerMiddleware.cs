using System.Text.Json;

namespace Catalog.Common.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            // If env is development, re-throw the exception so the UseDeveloperExceptionPage middleware can handle it.
            if (_environment.IsDevelopment()) throw;

            _logger.LogError(e, "an unhandled exception has occured while processing the request");

            if (context.Response.HasStarted)
            {
                _logger.LogError("The response header has been sent to the user, can not alter the response header in " +
                                 "the CustomExceptionLoggerHandlerMiddleware.");

                return;
            }
            
            var internalServerError = TypedResults.InternalServerError("An unhandled exception occured while processing the request");
            await internalServerError.ExecuteAsync(context);
        }
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}