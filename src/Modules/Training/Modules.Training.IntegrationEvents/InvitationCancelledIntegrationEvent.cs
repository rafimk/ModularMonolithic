using Application.EventBus;

namespace Modules.Training.IntegrationEvents;

public sealed record InvitationCancelledIntegrationEvent(Guid Id, DateTime OccurredOnUtc, Guid InvitationId) : IntegrationEvent(Id, OccurredOnUtc);