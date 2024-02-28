using Shared.Errors;
using Shared.Results;

namespace Application.Infrastructure;

internal static class ValidationResultFactory
{
    private static readonly Type ResultType = typeof(Result);
    private static readonly Type ValidationResultGenericTypeDefinition = typeof(ValidationResult<>).GetGenericTypeDefinition();

    internal static TResult Create<TResult>(Error[] errors)
        where TResult : Result =>
        typeof(TResult) == ResultType
            ? (ValidationResult.WithErrors(errors) as TResult)!
            : CreateGenericValidationResult<TResult>(typeof(TResult).GenericTypeArguments[0], errors);

    private static TResult CreateGenericValidationResult<TResult>(Type resultGenericType, Error[] errors) =>
        (TResult)ValidationResultGenericTypeDefinition
            .MakeGenericType(resultGenericType)
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;
}
