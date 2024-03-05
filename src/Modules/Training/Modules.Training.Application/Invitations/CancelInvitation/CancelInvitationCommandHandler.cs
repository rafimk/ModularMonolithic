using Application.Messaging;
using Modules.Training.Domain;
using Modules.Training.Domain.Invitations;
using Shared.Results;

namespace Modules.Training.Application.Invitations.CancelInvitation;

internal sealed class CancelInvitationCommandHandler : ICommandHandler<CancelInvitationCommand>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelInvitationCommandHandler(IInvitationRepository invitationRepository, IUnitOfWork unitOfWork)
    {
        _invitationRepository = invitationRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(CancelInvitationCommand request, CancellationToken cancellationToken) =>
        await GetInvitationById(new InvitationId(request.InvitationId), cancellationToken)
            .Bind(invitation => invitation.Cancel())
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken));

    private async Task<Result<Invitation>> GetInvitationById(InvitationId invitationId, CancellationToken cancellationToken) =>
        await _invitationRepository.GetByIdAsync(invitationId, cancellationToken)
            .MapFailure(() => InvitationErrors.NotFound(invitationId));
}