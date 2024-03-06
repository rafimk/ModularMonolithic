using Microsoft.Extensions.Options;
using Modules.Notifications.Application.Abstractions.Email;
using Modules.Notifications.Infrastructure.Email.Abstractions;
using Modules.Notifications.Infrastructure.Email.Configuration;
using Modules.Notifications.Infrastructure.Email.Contracts;

namespace Modules.Notifications.Infrastructure.Email;

internal sealed class EmailSender : IEmailSender
{
    private readonly IMailersendClient _mailersendClient;
    private readonly MailersendOptions _options;

    public EmailSender(IMailersendClient mailersendClient, IOptions<MailersendOptions> options)
    {
        _mailersendClient = mailersendClient;
        _options = options.Value;
    }

    public async Task SendWelcomeAsync(WelcomeEmailRequest welcomeEmailRequest, CancellationToken cancellationToken = default)
    {
        EmailRequest emailRequest = EmailRequest.Create(_options.Templates.WelcomeEmail)
            .WithSender(_options.SenderEmail)
            .WithRecipient(welcomeEmailRequest.Email)
            .WithVariable(
                Variable
                    .Create(welcomeEmailRequest.Email)
                    .WithSubstitution("name", welcomeEmailRequest.Name));

        await _mailersendClient.SendEmailAsync(emailRequest, cancellationToken);
    }

    public async Task SendClientInvitationAsync(
        ClientInvitationEmailRequest clientInvitationEmailRequest,
        CancellationToken cancellationToken = default)
    {
        EmailRequest emailRequest = EmailRequest.Create(_options.Templates.ClientInvitationEmail)
            .WithSender(_options.SenderEmail)
            .WithRecipient(clientInvitationEmailRequest.Email)
            .WithVariable(
                Variable
                    .Create(clientInvitationEmailRequest.Email)
                    .WithSubstitution("trainer", clientInvitationEmailRequest.Trainer)
                    .WithSubstitution("invitationId", clientInvitationEmailRequest.InvitationId.ToString()));

        await _mailersendClient.SendEmailAsync(emailRequest, cancellationToken);
    }
}
