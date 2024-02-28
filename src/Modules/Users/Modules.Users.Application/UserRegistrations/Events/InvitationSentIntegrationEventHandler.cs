using Application.EventBus;
using Microsoft.Extensions.Logging;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain;
using Shared.Results;


namespace Modules.Users.Application.UserRegistrations.Events;
internal sealed class InvitationSentIntegrationEventHandler : IntegrationEventHandler<InvitationSentIntegrationEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InvitationSentIntegrationEventHandler> _logger;

    public InvitationSentIntegrationEventHandler(
        IUserRegistrationRepository userRegistrationRepository,
        IUnitOfWork unitOfWork,
        ILogger<InvitationSentIntegrationEventHandler> logger)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public override async Task Handle(InvitationSentIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await UserRegistration.Create(new UserRegistrationId(integrationEvent.InvitationId), integrationEvent.Email)
            .Bind(userRegistration => CheckNoPendingInvitationsAsync(userRegistration, cancellationToken))
            .Tap(userRegistration => _userRegistrationRepository.Add(userRegistration))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(integrationEvent, error));

    private async Task<Result<UserRegistration>> CheckNoPendingInvitationsAsync(UserRegistration invitation, CancellationToken cancellationToken) =>
        await _userRegistrationRepository.CheckNonePendingForEmailAsync(invitation.Email, cancellationToken)
            .Map(() => invitation)
            .MapFailure(() => UserRegistrationErrors.PendingExists);
}