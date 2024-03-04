
using Shared.Errors;

namespace Modules.Training.Domain.Workouts;

public static class RestIntervalErrors
{
    public static Error Invalid => new("RestInterval.Invalid", "The provided value is not a valid rest interval");
}