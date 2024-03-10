using System.Security.Claims;

namespace Authorization.Extensions;

internal static class ClaimsPrincipalExtensions
{
    internal static string GetIdentityProviderId(this ClaimsPrincipal claimsPrincipal) =>
        claimsPrincipal.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
}