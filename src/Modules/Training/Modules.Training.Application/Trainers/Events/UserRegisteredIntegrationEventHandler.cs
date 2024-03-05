using Application.EventBus;
using Microsoft.Extensions.Logging;
using Modules.Training.Domain;
using Modules.Training.Domain.Trainers;
using Modules.Users.IntegrationEvents;
using Shared.Results;

namespace Modules.Training.Application.Trainers.Events;

internal sealed class UserRegisteredIntegrationEventHandler : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;

    public UserRegisteredIntegrationEventHandler(
        ITrainerRepository trainerRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserRegisteredIntegrationEventHandler> logger)
    {
        _trainerRepository = trainerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task Handle(UserRegisteredIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await Result.Create(integrationEvent)
            .Filter(userRegisteredIntegrationEvent => userRegisteredIntegrationEvent.Roles.Contains(Trainer.Role))
            .Bind(userRegisteredIntegrationEvent => Trainer.Create(
                new TrainerId(userRegisteredIntegrationEvent.UserId),
                userRegisteredIntegrationEvent.Email,
                userRegisteredIntegrationEvent.FirstName,
                userRegisteredIntegrationEvent.LastName))
            .Tap(trainer => _trainerRepository.Add(trainer))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(integrationEvent, error));
}