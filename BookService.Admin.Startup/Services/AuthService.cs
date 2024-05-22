using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookService.Admin.Startup.Services;

public class AuthService(IHttpContextAccessor contextAccessor) : IAuthService
{
    public async Task Authorize(string email, string name, string role, string id, DateTime expiredAtUtc)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new("Name", name),
            new(ClaimTypes.Role, role),
            new(ClaimTypes.NameIdentifier, id)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = expiredAtUtc,
        };

        await contextAccessor.HttpContext?.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties)!;
    }
}