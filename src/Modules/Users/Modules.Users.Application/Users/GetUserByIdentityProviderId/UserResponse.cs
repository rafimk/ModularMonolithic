﻿namespace Modules.Users.Application.Users.GetUserByIdentityProviderId;

public sealed record UserResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public HashSet<string> Permissions { get; init; } = new();
}