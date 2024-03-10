using Microsoft.AspNetCore.Mvc;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Contracts.UserRegistrations;

public sealed class ConfirmUserRegistrationRequest
{
    [FromRoute(Name = UserRegistrationsRoutes.ResourceId)]
    public Guid UserRegistrationId { get; init; }

    [FromBody]
    public ConfirmUserRegistrationRequestContent Content { get; init; } = new(string.Empty, string.Empty, string.Empty);
}