using Modules.Notifications.Infrastructure.Email.Contracts;
using Refit;

namespace Modules.Notifications.Infrastructure.Email.Abstractions;

internal interface IMailersendClient
{
    [Post("/email")]
    Task SendEmailAsync(EmailRequest request, CancellationToken cancellationToken = default);
}