using Application.Messaging;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain.Users;
using Modules.Users.Domain;
using Shared.Results;

namespace Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;

internal sealed class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand>
{
    private readonly IIdentityProviderService _identityProviderService;
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmUserRegistrationCommandHandler(
        IIdentityProviderService identityProviderService,
        IUserRegistrationRepository userRegistrationRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _identityProviderService = identityProviderService;
        _userRegistrationRepository = userRegistrationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken) =>
        await GetUserRegistrationByIdAsync(new UserRegistrationId(request.UserRegistrationId), cancellationToken)
            .Bind(userRegistration => CheckIfIdentityExistsAsync(userRegistration, request.IdentityProviderId, cancellationToken))
            .Bind(userRegistration => CheckIfEmailIsUniqueAsync(userRegistration, cancellationToken))
            .Bind(userRegistration => userRegistration.Confirm(
                request.IdentityProviderId,
                request.Email,
                request.FirstName,
                request.LastName))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken));

    private async Task<Result<UserRegistration>> GetUserRegistrationByIdAsync(
        UserRegistrationId userRegistrationId,
        CancellationToken cancellationToken) =>
        await _userRegistrationRepository.GetByIdAsync(userRegistrationId, cancellationToken)
            .MapFailure(() => UserRegistrationErrors.NotFound(userRegistrationId));

    private async Task<Result<UserRegistration>> CheckIfIdentityExistsAsync(
        UserRegistration userRegistration,
        string identityProviderId,
        CancellationToken cancellationToken) =>
        await _identityProviderService.ExistsAsync(identityProviderId, cancellationToken)
            .Map(() => userRegistration)
            .MapFailure(() => UserRegistrationErrors.NotFoundByIdentity(identityProviderId));

    private async Task<Result<UserRegistration>> CheckIfEmailIsUniqueAsync(
        UserRegistration userRegistration,
        CancellationToken cancellationToken) =>
        await _userRepository.IsEmailUniqueAsync(userRegistration.Email, cancellationToken)
            .Map(() => userRegistration)
            .MapFailure(() => UserRegistrationErrors.EmailHasBeenTaken);
}