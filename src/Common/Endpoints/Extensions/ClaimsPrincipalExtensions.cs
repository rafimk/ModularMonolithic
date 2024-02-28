using System.Security.Claims;

namespace Endpoints.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetIdentityProviderId(this ClaimsPrincipal claimsPrincipal) =>
        claimsPrincipal.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
}