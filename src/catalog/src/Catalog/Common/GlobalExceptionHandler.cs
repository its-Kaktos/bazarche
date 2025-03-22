using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.Common;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    {
        _env = env;
        _logger = logger;
    }

    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, UnhandledExceptionMsg);

        if (context.Response.HasStarted)
        {
            _logger.LogError("The response header has been sent to the user, can not alter the response headers");

            return true;
        }

        if (_env.IsProduction())
        {
            var internalServerError = TypedResults.InternalServerError("An unhandled exception occured while processing the request");
            await internalServerError.ExecuteAsync(context);
        }
        else if (_env.IsDevelopment() || _env.IsStaging())
        {
            var internalServerError = TypedResults.InternalServerError(JsonSerializer.Serialize(exception));
            await internalServerError.ExecuteAsync(context);
        }
        else
        {
            _logger.LogError("an unhandled environment found {env}", _env.EnvironmentName);

            var internalServerError = TypedResults.InternalServerError("An unhandled exception occured while processing the request");
            await internalServerError.ExecuteAsync(context);
        }

        return true;
    }
}