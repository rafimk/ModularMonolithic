using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Domain.Trainers;
using Shared.Results;

namespace Modules.Training.Persistence.Repositories;

internal sealed class TrainerRepository : ITrainerRepository, IScoped
{
    private readonly TrainingDbContext _dbContext;

    public TrainerRepository(TrainingDbContext dbContext) => _dbContext = dbContext;

    public async Task<Result<Trainer>> GetByIdAsync(TrainerId id, CancellationToken cancellationToken = default) =>
        Result.Create(await _dbContext.Set<Trainer>().FirstOrDefaultAsync(user => user.Id == id, cancellationToken));

    public async Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken) =>
        Result.Create(!await _dbContext.Set<Trainer>().AnyAsync(trainer => trainer.Email == email, cancellationToken));

    public void Add(Trainer trainer) => _dbContext.Set<Trainer>().Add(trainer);
}