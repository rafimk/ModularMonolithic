using Microsoft.Extensions.Options;
using Modules.Notifications.Infrastructure.Email.Configuration;
using System.Net.Http.Headers;

namespace Modules.Notifications.Infrastructure.Email.DelegatingHandlers;

internal sealed class MailersendAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly MailersendOptions _options;

    public MailersendAuthorizationDelegatingHandler(IOptions<MailersendOptions> options) => _options = options.Value;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}