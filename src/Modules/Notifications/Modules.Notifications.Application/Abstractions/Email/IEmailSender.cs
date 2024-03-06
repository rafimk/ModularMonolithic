namespace Modules.Notifications.Application.Abstractions.Email;

public interface IEmailSender
{
    Task SendWelcomeAsync(WelcomeEmailRequest welcomeEmailRequest, CancellationToken cancellationToken = default);

    Task SendClientInvitationAsync(ClientInvitationEmailRequest clientInvitationEmailRequest, CancellationToken cancellationToken = default);
}
