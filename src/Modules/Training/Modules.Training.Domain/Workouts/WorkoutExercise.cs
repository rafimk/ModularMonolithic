
namespace Modules.Training.Domain.Workouts;

public sealed class WorkoutExercise
{

    internal WorkoutExercise(
        WorkoutId workoutId,
        ExerciseId exerciseId,
        Goal goal,
        RestInterval restInterval,
        int sets,
        int[] repetitions)
    {
        WorkoutId = workoutId;
        ExerciseId = exerciseId;
        RestInterval = restInterval;
        Sets = sets;
        Repetitions = repetitions;
        Goal = goal;
    }

    public WorkoutId WorkoutId { get; private set; }

    public ExerciseId ExerciseId { get; private set; }

    public Goal Goal { get; private set; }

    public RestInterval RestInterval { get; private set; }

    public int Sets { get; private set; }

    public int[] Repetitions { get; private set; }
}