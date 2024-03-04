using Domain.Primitives;

namespace Modules.Training.Domain.Invitations;

public sealed class Sender : ValueObject
{
    public Sender(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    private Sender()
    {
    }

    public string FirstName { get; } = string.Empty;

    public string LastName { get; } = string.Empty;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
    }
}