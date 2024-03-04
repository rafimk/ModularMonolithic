using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

public sealed record WorkoutId(Guid Value) : IEntityId;