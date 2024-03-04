using Shared.Results;

namespace Modules.Training.Domain.Invitations;

public interface IInvitationRepository
{
    Task<Result<Invitation>> GetByIdAsync(InvitationId id, CancellationToken cancellationToken = default);

    Task<Result> CheckNonePendingForEmailAsync(string email, CancellationToken cancellationToken = default);

    void Add(Invitation invitation);
}