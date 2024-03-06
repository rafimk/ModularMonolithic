using Application.EventBus;
using Modules.Notifications.Application.Abstractions.Email;
using Modules.Training.IntegrationEvents;

namespace Modules.Notifications.Application.Invitations;

internal sealed class InvitationSentIntegrationEventHandler : IntegrationEventHandler<InvitationSentIntegrationEvent>
{
    private readonly IEmailSender _emailSender;

    public InvitationSentIntegrationEventHandler(IEmailSender emailSender) => _emailSender = emailSender;

    public override async Task Handle(InvitationSentIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await _emailSender.SendClientInvitationAsync(
            new ClientInvitationEmailRequest(
                integrationEvent.Email,
                integrationEvent.InvitationId,
                $"{integrationEvent.SenderFirstName} {integrationEvent.SenderLastName}"),
            cancellationToken);
}