using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

public sealed class RestIntervalType : Enumeration<RestIntervalType>
{
    public static readonly RestIntervalType Seconds = new(1, "Seconds");
    public static readonly RestIntervalType Minutes = new(2, "Minutes");

    private RestIntervalType(int id, string name)
        : base(id, name)
    {
    }
}