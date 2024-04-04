using MimeKit;

namespace BookService.Admin.Startup.Services.Models;

public class Message
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsHtml { get; set; }

    public Message(IEnumerable<string> to, string subject, string content, bool isHtml = false)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(x => new MailboxAddress("Сервис бронирования столиков", x)));
        Subject = subject;
        Content = content;
        IsHtml = isHtml;
    }
}