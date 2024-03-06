namespace Modules.Notifications.Infrastructure.Email.Configuration;

internal sealed class MailersendOptions
{
    public string BaseUrl { get; init; } = string.Empty;

    public string AccessToken { get; init; } = string.Empty;

    public string SenderEmail { get; init; } = string.Empty;

    public MailersendTemplates Templates { get; init; } = new();
}