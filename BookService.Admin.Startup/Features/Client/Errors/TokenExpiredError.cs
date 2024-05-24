using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Client.Errors;

public class TokenExpiredError : Error
{
    public override string Type => nameof(TokenExpiredError);
    public static TokenExpiredError Instance => new();
}