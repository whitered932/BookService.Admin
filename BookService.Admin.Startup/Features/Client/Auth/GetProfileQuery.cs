using System.Security.Claims;
using BookService.Admin.Startup.Features.Client.Auth.Errors;
using BookService.Admin.Startup.Features.Client.Auth.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Client.Auth;

public class GetProfileQuery : Query<ClientProfileDto>
{
}

public sealed class GetProfileQueryHandler(IAuthorizationTokenRepository authorizationTokenRepository, IClientRepository clientRepository, IHttpContextAccessor contextAccessor)  : QueryHandler<GetProfileQuery, ClientProfileDto>
{
    public override async Task<Result<ClientProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userEmail = contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
        var client = await clientRepository.SingleOrDefaultAsync(x => x.Email == userEmail, cancellationToken);
        if (client is null)
        {
            return Error(TokenExpiredError.Instance);
        }

        var result = new ClientProfileDto()
        {
            Email = client.Email,
            Name = client.Name,
            PhoneNumber = client.PhoneNumber
        };
        return Successful(result);
    }
}

