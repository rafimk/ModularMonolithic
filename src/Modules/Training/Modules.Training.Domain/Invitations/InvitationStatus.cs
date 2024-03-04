using Domain.Primitives;

namespace Modules.Training.Domain.Invitations;

public sealed class InvitationStatus : Enumeration<InvitationStatus>
{
    public static readonly InvitationStatus Pending = new(1, "Pending");
    public static readonly InvitationStatus Accepted = new(2, "Accepted");
    public static readonly InvitationStatus Cancelled = new(3, "Cancelled");
    public static readonly InvitationStatus Expired = new(4, "Expired");

    private InvitationStatus(int id, string name)
        : base(id, name)
    {
    }

    private InvitationStatus(int id)
    {
        InvitationStatus? userRegistrationStatus = FromId(id);

        Id = userRegistrationStatus?.Id ?? throw new ArgumentNullException(nameof(id));
        Name = userRegistrationStatus?.Name ?? throw new ArgumentNullException(nameof(id));
    }
}