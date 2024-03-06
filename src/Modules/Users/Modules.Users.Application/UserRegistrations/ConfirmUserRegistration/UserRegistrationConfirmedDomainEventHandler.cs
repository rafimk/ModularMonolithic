using Application.Messaging;
using Microsoft.Extensions.Logging;
using Modules.Users.Domain.UserRegistrations.Events;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain.Users;
using Modules.Users.Domain;
using Shared.Results;
using Application.Extensions;


namespace Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;

internal sealed class UserRegistrationConfirmedDomainEventHandler : IDomainEventHandler<UserRegistrationConfirmedDomainEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserRegistrationConfirmedDomainEventHandler> _logger;

    public UserRegistrationConfirmedDomainEventHandler(
        IUserRegistrationRepository userRegistrationRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserRegistrationConfirmedDomainEventHandler> logger)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(UserRegistrationConfirmedDomainEvent notification, CancellationToken cancellationToken) =>
        await GetUserRegistrationByIdAsync(notification.UserRegistrationId, cancellationToken)
            .Bind(User.CreateFromRegistration)
            .Tap(user => _userRepository.Add(user))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(notification, error));

    private async Task<Result<UserRegistration>> GetUserRegistrationByIdAsync(
        UserRegistrationId userRegistrationId,
        CancellationToken cancellationToken) =>
        await _userRegistrationRepository.GetByIdAsync(userRegistrationId, cancellationToken)
            .MapFailure(() => UserRegistrationErrors.NotFound(userRegistrationId));
}