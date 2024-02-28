using Application.EventBus;
using Microsoft.Extensions.Logging;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain;
using Shared.Results;

namespace Modules.Users.Application.UserRegistrations.Events;

internal sealed class InvitationCancelledIntegrationEventHandler : IntegrationEventHandler<InvitationCancelledIntegrationEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InvitationCancelledIntegrationEventHandler> _logger;

    public InvitationCancelledIntegrationEventHandler(
        IUserRegistrationRepository userRegistrationRepository,
        IUnitOfWork unitOfWork,
        ILogger<InvitationCancelledIntegrationEventHandler> logger)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task Handle(InvitationCancelledIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await GetUserRegistrationByIdAsync(new UserRegistrationId(integrationEvent.InvitationId), cancellationToken)
            .Bind(userRegistration => userRegistration.Cancel())
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(integrationEvent, error));

    private async Task<Result<UserRegistration>> GetUserRegistrationByIdAsync(
        UserRegistrationId userRegistrationId,
        CancellationToken cancellationToken) =>
        await _userRegistrationRepository.GetByIdAsync(userRegistrationId, cancellationToken)
            .MapFailure(() => UserRegistrationErrors.NotFound(userRegistrationId));
}