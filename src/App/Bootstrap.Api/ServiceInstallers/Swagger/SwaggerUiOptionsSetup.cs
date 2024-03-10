using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Bootstrap.Api.ServiceInstallers.Swagger;

internal sealed class SwaggerUiOptionsSetup : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options) => options.DisplayRequestDuration();
}