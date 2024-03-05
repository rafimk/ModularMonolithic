using System;

namespace Modules.Training.Application.Invitations.GetInvitations;

public sealed record InvitationResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; } = string.Empty;

    public int Status { get; init; }

    public DateTime CreatedOnUtc { get; init; }

    public DateTime? ModifiedOnUtc { get; init; }
}