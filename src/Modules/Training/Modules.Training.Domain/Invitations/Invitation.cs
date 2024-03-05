using Domain.Primitives;
using Domain.Time;
using Modules.Training.Domain.Clients;
using Modules.Training.Domain.Invitations.Events;
using Modules.Training.Domain.Trainers;
using Shared.Results;

namespace Modules.Training.Domain.Invitations;

public sealed class Invitation : Entity<InvitationId>, IAuditable
{
    private Invitation(InvitationId id, TrainerId trainerId, string email, Sender sender, InvitationStatus status)
        : this(id, trainerId, email, status) =>
        Sender = sender;

    private Invitation(InvitationId id, TrainerId trainerId, string email, InvitationStatus status)
         : base(id)
    {
        TrainerId = trainerId;
        Email = email;
        Status = status;
        Sender = new Sender(string.Empty, string.Empty);
    }

    public TrainerId TrainerId { get; private set; }

    public string Email { get; private set; }

    public Sender Sender { get; private set; }

    public InvitationStatus Status { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public Result<Client> Accept(ClientId clientId, string email, string firstName, string lastName)
    {
        if (Email != email)
        {
            return Result.Failure<Client>(InvitationErrors.EmailDoesNotMatch);
        }

        return Status switch
        {
            var status when status == InvitationStatus.Cancelled => Result.Failure<Client>(InvitationErrors.Cancelled(Id)),
            var status when status == InvitationStatus.Expired => Result.Failure<Client>(InvitationErrors.Expired(Id)),
            _ => AcceptInternal(clientId, email, firstName, lastName)
        };
    }

    public Result Cancel() =>
        Status switch
        {
            var status when status == InvitationStatus.Cancelled => Result.Failure<Client>(InvitationErrors.Cancelled(Id)),
            var status when status == InvitationStatus.Expired => Result.Failure<Client>(InvitationErrors.Expired(Id)),
            _ => CancelInternal()
        };

    internal static Result<Invitation> Create(Trainer trainer, string email)
    {
        var invitation = new Invitation(
            new InvitationId(Guid.NewGuid()),
            trainer.Id,
            email,
            new Sender(trainer.FirstName, trainer.LastName),
            InvitationStatus.Pending);

        invitation.RaiseDomainEvent(new InvitationCreatedDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            invitation.Id,
            invitation.TrainerId,
            invitation.Email,
            invitation.Sender));

        return invitation;
    }

    private Result<Client> AcceptInternal(ClientId clientId, string email, string firstName, string lastName)
    {
        Status = InvitationStatus.Accepted;

        return Client.Create(clientId, TrainerId, email, firstName, lastName);
    }

    private Result CancelInternal()
    {
        Status = InvitationStatus.Cancelled;

        RaiseDomainEvent(new InvitationCancelledDomainEvent(Guid.NewGuid(), SystemTimeProvider.UtcNow(), Id));

        return Result.Success();
    }
}