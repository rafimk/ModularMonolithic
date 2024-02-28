
using Shared.Errors;

namespace Shared.Results;

public interface IValidationResult
{
    public static readonly Error ValidationError = new("ValidationError", "A validation problem occurred.");

    /// <summary>
    /// Gets the errors.
    /// </summary>
    Error[] Errors { get; }
}