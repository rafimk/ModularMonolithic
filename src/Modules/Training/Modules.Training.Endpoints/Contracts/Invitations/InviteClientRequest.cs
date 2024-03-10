using Microsoft.AspNetCore.Mvc;
using Modules.Training.Endpoints.Routes;

namespace Modules.Training.Endpoints.Contracts.Invitations;

public sealed class InviteClientRequest
{
    [FromRoute(Name = TrainersRoutes.ResourceId)]
    public Guid TrainerId { get; init; }

    [FromBody]
    public InviteClientRequestContent Content { get; init; } = new(string.Empty);
}