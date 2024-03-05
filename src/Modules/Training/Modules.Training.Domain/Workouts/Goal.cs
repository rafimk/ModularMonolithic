using Domain.Primitives;
using Shared.Results;

namespace Modules.Training.Domain.Workouts;

public sealed class Goal : ValueObject
{
    private Goal(GoalType type, string value)
    {
        Type = type;
        Value = value;
    }

    public GoalType Type { get; }

    public string Value { get; }

    public static Result<Goal> Create(GoalType type, string value) => Result.Success(new Goal(type, value));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Type.Id;
        yield return Value;
    }
}
