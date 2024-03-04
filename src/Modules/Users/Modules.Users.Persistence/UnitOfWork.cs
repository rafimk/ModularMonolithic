using Application.ServiceLifetimes;
using Modules.Users.Domain;

namespace Modules.Users.Persistence;

internal sealed class UnitOfWork : IUnitOfWork, IScoped
{
    private readonly UsersDbContext _dbContext;

    public UnitOfWork(UsersDbContext dbContext) => _dbContext = dbContext;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}