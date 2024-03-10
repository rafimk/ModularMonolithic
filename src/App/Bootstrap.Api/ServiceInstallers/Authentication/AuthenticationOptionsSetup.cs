using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Bootstrap.Api.ServiceInstallers.Authentication;

internal sealed class AuthenticationOptionsSetup : IConfigureOptions<AuthenticationOptions>
{
    public void Configure(AuthenticationOptions options) => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}