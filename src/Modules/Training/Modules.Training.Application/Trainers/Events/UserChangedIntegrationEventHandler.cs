using Application.EventBus;
using Application.Extensions;
using Microsoft.Extensions.Logging;
using Modules.Training.Domain;
using Modules.Training.Domain.Trainers;
using Modules.Users.IntegrationEvents;
using Shared.Results;

namespace Modules.Training.Application.Trainers.Events;

internal sealed class UserChangedIntegrationEventHandler : IntegrationEventHandler<UserChangedIntegrationEvent>
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserChangedIntegrationEvent> _logger;

    public UserChangedIntegrationEventHandler(
        ITrainerRepository trainerRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserChangedIntegrationEvent> logger)
    {
        _trainerRepository = trainerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task Handle(UserChangedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await Result.Create(integrationEvent)
            .Filter(userRegisteredIntegrationEvent => userRegisteredIntegrationEvent.Roles.Contains(Trainer.Role))
            .Bind(() => GetTrainerByIdAsync(new TrainerId(integrationEvent.UserId), cancellationToken))
            .Tap(trainer => trainer.Change(integrationEvent.FirstName, integrationEvent.LastName))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(integrationEvent, error));

    private async Task<Result<Trainer>> GetTrainerByIdAsync(TrainerId trainerId, CancellationToken cancellationToken) =>
        await _trainerRepository.GetByIdAsync(trainerId, cancellationToken)
            .MapFailure(() => TrainerErrors.NotFound(trainerId));
}