// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Catalog.Common;

public sealed class FailedResultType
{
    public required string ErrorType { get; init; }
    public required string Message { get; init; }
    public required string TraceIdentifier { get; init; }
    public IDictionary<string, List<string>>? Errors { get; init; }
}