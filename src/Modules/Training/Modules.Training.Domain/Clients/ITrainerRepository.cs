using Shared.Results;

namespace Modules.Training.Domain.Clients;

public interface IClientRepository
{
    Task<Result<Client>> GetByIdAsync(ClientId id, CancellationToken cancellationToken = default);

    Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
    void Add(Client client);
}