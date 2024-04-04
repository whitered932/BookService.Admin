using BookService.Admin.Startup.Services.Models;

namespace BookService.Admin.Startup.Services;

public interface IEmailSenderService
{
    void Send(Message message);
    Task SendAsync(Message message);
}