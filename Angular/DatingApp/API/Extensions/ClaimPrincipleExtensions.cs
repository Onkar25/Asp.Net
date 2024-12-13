using System;
using System.Security.Claims;

namespace API.Extensions;

public static class ClaimPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal principal)
    {
        var username = principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Cannot get username from token");
        return username;
    }
}
