using System;
using System.Security.Claims;

namespace BlazorSelfHostedAuthWithSignalr.Client.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? FindFirstValue(this ClaimsPrincipal principal, string claimType)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        var claim = principal.FindFirst(claimType);
        return claim is not null ? claim.Value : null;
    }
}
