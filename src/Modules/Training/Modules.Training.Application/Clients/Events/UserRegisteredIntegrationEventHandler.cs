﻿using Application.EventBus;
using Application.Extensions;
using Microsoft.Extensions.Logging;
using Modules.Training.Domain;
using Modules.Training.Domain.Clients;
using Modules.Training.Domain.Invitations;
using Modules.Users.IntegrationEvents;
using Shared.Results;

namespace Modules.Training.Application.Clients.Events;

internal sealed class UserRegisteredIntegrationEventHandler : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;

    public UserRegisteredIntegrationEventHandler(
        IInvitationRepository invitationRepository,
        IClientRepository clientRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserRegisteredIntegrationEventHandler> logger)
    {
        _invitationRepository = invitationRepository;
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public override async Task Handle(UserRegisteredIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await Result.Create(integrationEvent)
            .Filter(userRegisteredIntegrationEvent =>
                userRegisteredIntegrationEvent.Roles.Contains(Client.Role))
            .Filter(userRegisteredIntegrationEvent =>
                userRegisteredIntegrationEvent.UserRegistrationId.HasValue)
            .Bind(userRegisteredIntegrationEvent =>
                GetInvitationById(
                    new InvitationId(userRegisteredIntegrationEvent.UserRegistrationId!.Value),
                    cancellationToken))
            .Bind(invitation => invitation.Accept(
                new ClientId(integrationEvent.UserId),
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName))
            .Tap(client => _clientRepository.Add(client))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(integrationEvent, error));

    private async Task<Result<Invitation>> GetInvitationById(InvitationId invitationId, CancellationToken cancellationToken) =>
        await _invitationRepository.GetByIdAsync(invitationId, cancellationToken)
            .MapFailure(() => InvitationErrors.NotFound(invitationId));
}
