using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

public sealed class Exercise : Entity<ExerciseId>, IAuditable
{
    public Exercise(ExerciseId id, string name, string? description = null)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }
}