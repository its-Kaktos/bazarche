using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.Common;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not CatalogValidationException) return false;

        var badRequest = TypedResults.BadRequest(exception);
        await badRequest.ExecuteAsync(context);

        return true;
    }
}