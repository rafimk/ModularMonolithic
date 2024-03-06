using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Notifications.Infrastructure.Email.Contracts;

internal sealed record EmailRequest
{
    private EmailRequest()
    {
    }

    public Sender From { get; init; } = new(string.Empty);

    public Recipient[] To { get; init; } = Array.Empty<Recipient>();

    public Variable[] Variables { get; init; } = Array.Empty<Variable>();

    public string TemplateId { get; init; } = string.Empty;

    public static EmailRequest Create(string templateId) => new()
    {
        TemplateId = templateId
    };

    public EmailRequest WithSender(string email) => this with
    {
        From = new Sender(email)
    };

    public EmailRequest WithRecipient(string email) => this with
    {
        To = new List<Recipient> { new(email) }
            .Tap(list => list.AddRange(To))
            .ToArray()
    };

    public EmailRequest WithVariable(Variable variable) => this with
    {
        Variables = new List<Variable> { variable }
            .Tap(list => list.AddRange(Variables))
            .ToArray()
    };
}