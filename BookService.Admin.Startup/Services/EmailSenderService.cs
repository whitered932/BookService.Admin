using BookService.Admin.Startup.Services.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace BookService.Admin.Startup.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly ILogger<EmailSenderService> _logger;

    public class Credentials
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    private readonly Credentials _emailConfig;

    public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger)
    {
        _logger = logger;
        _emailConfig = configuration.GetSection("Email").Get<Credentials>();
    }

    
    public void Send(Message message)
    {
        _logger.LogInformation($"Sending email to {message.To.FirstOrDefault()}");
        Send(CreateEmailMessage(message));
    }

    public async Task SendAsync(Message message)
    {
        _logger.LogInformation($"Sending email to {message.To.FirstOrDefault()}");
        await SendAsync(CreateEmailMessage(message));
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Сервис бронирования столиков", _emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        var bodyBuilder = new BodyBuilder();
        if (message.IsHtml)
        {
            bodyBuilder.HtmlBody = message.Content;
        }
        else
        {
            bodyBuilder.TextBody = message.Content;
        }

        emailMessage.Body = bodyBuilder.ToMessageBody();
        return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.CheckCertificateRevocation = false; 
            client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            _logger.LogInformation($"Connecting to {_emailConfig.SmtpServer}:{_emailConfig.Port}, useSsl: {false}");
            client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfig.Username, _emailConfig.Password);
            _logger.LogInformation($"Sending start");
            client.Send(mailMessage);
            _logger.LogInformation($"Sended successfully");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"Exception while sending");
            throw;
        }
        finally
        {
            _logger.LogInformation($"End sending");
            client.Disconnect(true);
            client.Dispose();
        }
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.CheckCertificateRevocation = false;
            client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            _logger.LogInformation($"Connecting to {_emailConfig.SmtpServer}:{_emailConfig.Port}, useSsl: {false}");
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
            _logger.LogInformation($"Connection success");
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
            _logger.LogInformation($"Auth success");
            await client.SendAsync(mailMessage);
            _logger.LogInformation($"Sended successfully");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"Exception while sending");
            //log an error message or throw an exception or both.
            throw;
        }
        finally
        {
            _logger.LogInformation($"End sending");
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}