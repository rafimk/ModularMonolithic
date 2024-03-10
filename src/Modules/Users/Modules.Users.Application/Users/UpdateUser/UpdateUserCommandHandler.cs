using Application.Messaging;
using Modules.Users.Domain;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken) =>
        await GetUserByIdAsync(new UserId(request.UserId), cancellationToken)
            .Tap(user => user.Change(request.FirstName, request.LastName))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken));

    private async Task<Result<User>> GetUserByIdAsync(UserId userId, CancellationToken cancellationToken) =>
        await _userRepository.GetByIdAsync(userId, cancellationToken)
            .MapFailure(() => UserErrors.NotFound(userId));
}