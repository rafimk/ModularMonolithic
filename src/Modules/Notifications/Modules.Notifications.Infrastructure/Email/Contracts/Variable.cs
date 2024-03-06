using Shared.Extensions;

namespace Modules.Notifications.Infrastructure.Email.Contracts;

internal sealed record Variable
{
    private Variable()
    {
    }

    public string Email { get; init; } = string.Empty;

    public Substitution[] Substitutions { get; init; } = Array.Empty<Substitution>();

    public static Variable Create(string email) => new()
    {
        Email = email
    };

    public Variable WithSubstitution(string var, string value) => this with
    {
        Substitutions = new List<Substitution> { new(var, value) }
            .Tap(list => list.AddRange(Substitutions))
            .ToArray()
    };
}