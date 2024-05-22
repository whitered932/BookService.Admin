using System.Security.Claims;
using BookService.Admin.Startup.Features.Client.Auth.Errors;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookService.Admin.Startup.Features.Auth;

public class LoginCommand : Command
{
    public string Token { get; set; }
}

public sealed class LoginCommandHandler(IAuthorizationTokenRepository authorizationTokenRepository, IClientRepository clientRepository, IHttpContextAccessor contextAccessor) : CommandHandler<LoginCommand>
{
    public override async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var token = await authorizationTokenRepository.SingleOrDefaultAsync(x => x.Value == request.Token, cancellationToken);
        if (token is null || !token.IsValid(now))
        {
            return Error(TokenExpiredError.Instance);
        }

        var client = await clientRepository.SingleOrDefaultAsync(x => x.Id == token.UserId, cancellationToken);
        if (client is null)
        {
            return Error(TokenExpiredError.Instance);
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, client.Email),
            new("Name", client.Name),
            new(ClaimTypes.Role, "Client"),
            new(ClaimTypes.NameIdentifier, client.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = token.ExpiresAtUtc,
        };

        await contextAccessor.HttpContext?.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties)!;
        return Successful();
    }
}