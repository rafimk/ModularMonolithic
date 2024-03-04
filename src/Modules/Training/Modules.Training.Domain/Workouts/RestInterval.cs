using Domain.Primitives;
using Shared.Results;

namespace Modules.Training.Domain.Workouts;

public sealed class RestInterval : ValueObject
{
    private RestInterval(RestIntervalType type, int value)
    {
        Type = type;
        Value = value;
    }

    public RestIntervalType Type { get; }

    public int Value { get; }

    public static Result<RestInterval> Create(RestIntervalType type, int value) =>
        value >= 0 ?
            Result.Success(new RestInterval(type, value)) :
            Result.Failure<RestInterval>(RestIntervalErrors.Invalid);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Type.Id;
        yield return Value;
    }
}