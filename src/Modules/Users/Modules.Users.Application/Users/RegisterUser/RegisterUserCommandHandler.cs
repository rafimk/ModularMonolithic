using Application.Messaging;
using Modules.Users.Domain;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IIdentityProviderService _identityProviderService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IIdentityProviderService identityProviderService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _identityProviderService = identityProviderService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken) =>
        await User.Create(
                new UserId(Guid.NewGuid()),
                request.IdentityProviderId,
                request.Email,
                request.FirstName,
                request.LastName)
            .Bind(user => CheckIfIdentityExistsAsync(user, cancellationToken))
            .Bind(user => CheckIfEmailIsUniqueAsync(user, cancellationToken))
            .Tap(user => _userRepository.Add(user))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .Map(user => user.Id.Value);

    private async Task<Result<User>> CheckIfIdentityExistsAsync(User user, CancellationToken cancellationToken) =>
        await _identityProviderService.ExistsAsync(user.IdentityProviderId, cancellationToken)
            .Map(() => user)
            .MapFailure(() => UserErrors.NotFoundByIdentity(user.IdentityProviderId));

    private async Task<Result<User>> CheckIfEmailIsUniqueAsync(User user, CancellationToken cancellationToken) =>
        await _userRepository.IsEmailUniqueAsync(user.Email, cancellationToken)
            .Map(() => user)
            .MapFailure(() => UserErrors.EmailIsNotUnique);
}