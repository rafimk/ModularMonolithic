using Shared.Errors;

namespace Modules.Training.Domain.Clients;

public static class ClientErrors
{
    public static Func<ClientId, Error> NotFound => trainerId => new NotFoundError(
        "Client.NotFound",
        $"The client with the identifier '{trainerId.Value}' was not found.");
}