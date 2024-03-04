using Shared.Errors;

namespace Modules.Training.Domain.Trainers;

public static class TrainerErrors
{
    public static Func<TrainerId, Error> NotFound => trainerId => new NotFoundError(
        "Trainer.NotFound",
        $"The trainer with the identifier '{trainerId.Value}' was not found.");
}
