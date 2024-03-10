using Microsoft.AspNetCore.Mvc;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Contracts.Users;

public sealed class UpdateUserRequest
{
    [FromRoute(Name = UsersRoutes.ResourceId)]
    public Guid UserId { get; init; }

    [FromBody]
    public UpdateUserRequestContent Content { get; init; } = new(string.Empty, string.Empty);
}