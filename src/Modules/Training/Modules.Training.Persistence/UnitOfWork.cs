using Application.ServiceLifetimes;
using Modules.Training.Domain;

namespace Modules.Training.Persistence;

internal sealed class UnitOfWork : IUnitOfWork, IScoped
{
    private readonly TrainingDbContext _dbContext;

    public UnitOfWork(TrainingDbContext dbContext) => _dbContext = dbContext;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}