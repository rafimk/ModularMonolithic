using Domain.Primitives;
using Modules.Training.Domain.Invitations;
using Shared.Results;

namespace Modules.Training.Domain.Trainers;

public sealed class Trainer : Entity<TrainerId>, IAuditable
{
    public const string Role = "Trainer";

    private Trainer(TrainerId id, string email, string firstName, string lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }

    public static Result<Trainer> Create(TrainerId id, string email, string firstName, string lastName)
    {
        var trainer = new Trainer(id, email, firstName, lastName);

        return trainer;
    }

    public void Change(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Result<Invitation> Invite(string email) => Invitation.Create(this, email);
}