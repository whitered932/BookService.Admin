using BookService.Admin.Startup.Features.Client.Errors;
using BookService.Admin.Startup.Services;
using BookService.Admin.Startup.Services.Models;
using BookService.Domain.Models;
using BookService.Domain.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Client;

public class SendEmailCommand : Command
{
    public string Email { get; set; }
}

public sealed class SendEmailCommandHandler(
    IClientRepository clientRepository,
    IAuthorizationTokenRepository tokenRepository,
    IEmailSenderService emailSenderService,
    IHttpContextAccessor httpContextAccessor) : CommandHandler<SendEmailCommand>
{
    public override async Task<Result> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (client is null)
        {
            return Error(SendingError.Instance);
        }
        var value = Guid.NewGuid().ToString();
        var token = new AuthorizationToken(client.Id, value, DateTime.UtcNow.AddDays(1));
        await tokenRepository.AddAsync(token, cancellationToken);
        await tokenRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        var httpRequest = httpContextAccessor.HttpContext!.Request;
        var uri = httpRequest.Headers.Origin.FirstOrDefault();
        var message = new Message(new[] { client.Email }, "Вход в Сервис бронирования столиков",
            $"Ссылка для входа: {uri}/auth?token={token.Value}");
        await emailSenderService.SendAsync(message);
        return Successful();
    }
}