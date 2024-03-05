
namespace Modules.Training.Application.Invitations.GetInvitationById;

public sealed record InvitationResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; } = string.Empty;

    public string SenderFirstName { get; init; } = string.Empty;

    public string SenderLastName { get; init; } = string.Empty;

    public int Status { get; init; }
}