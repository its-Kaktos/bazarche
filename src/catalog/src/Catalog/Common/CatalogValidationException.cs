using FluentValidation.Results;

namespace Catalog.Common;

public class CatalogValidationException : Exception
{
    public CatalogValidationException(IEnumerable<ValidationFailure> failures) : base("validation failure have occured.")
    {
        Errors = failures
            .GroupBy(vr => vr.PropertyName, vf => vf.ErrorMessage)
            .ToDictionary(g => g.Key, x => x.ToList());
    }
    
    public IDictionary<string, List<string>> Errors { get; }
}