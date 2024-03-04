using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

public sealed class Workout : Entity<WorkoutId>, IAuditable
{
    public Workout(WorkoutId id, string name)
        : base(id) =>
        Name = name;

    public string Name { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }
}