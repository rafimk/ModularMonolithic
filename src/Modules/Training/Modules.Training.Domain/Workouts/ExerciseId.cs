using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

public sealed record ExerciseId(Guid Value) : IEntityId;