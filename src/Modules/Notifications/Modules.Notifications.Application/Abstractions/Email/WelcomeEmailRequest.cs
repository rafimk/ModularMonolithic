namespace Modules.Notifications.Application.Abstractions.Email;

public sealed record WelcomeEmailRequest(string Email, string Name);