using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Domain.Clients;
using Shared.Results;

namespace Modules.Training.Persistence.Repositories;

internal sealed class ClientRepository : IClientRepository, IScoped
{
    private readonly TrainingDbContext _dbContext;

    public ClientRepository(TrainingDbContext dbContext) => _dbContext = dbContext;

    public async Task<Result<Client>> GetByIdAsync(ClientId id, CancellationToken cancellationToken = default) =>
        Result.Create(await _dbContext.Set<Client>().FirstOrDefaultAsync(client => client.Id == id, cancellationToken));

    public async Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken) =>
        Result.Create(!await _dbContext.Set<Client>().AnyAsync(trainer => trainer.Email == email, cancellationToken));

    public void Add(Client client) => _dbContext.Set<Client>().Add(client);
}