using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

public sealed class GoalType : Enumeration<GoalType>
{
    public static readonly GoalType Weight = new(1, "Weight");
    public static readonly GoalType Time = new(2, "Time");
    public static readonly GoalType Distance = new(2, "Distance");

    private GoalType(int id, string name)
        : base(id, name)
    {
    }
}