using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Client.Auth.Errors;

public class SendingError : Error
{
    public override string Type => nameof(SendingError);
    public static SendingError Instance => new SendingError();
}