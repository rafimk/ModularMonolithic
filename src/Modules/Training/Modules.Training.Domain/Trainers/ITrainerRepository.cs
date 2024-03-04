using Shared.Results;

namespace Modules.Training.Domain.Trainers;

public interface ITrainerRepository
{
    Task<Result<Trainer>> GetByIdAsync(TrainerId id, CancellationToken cancellationToken = default);
    Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
    void Add(Trainer trainer);
}