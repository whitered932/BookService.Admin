using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Reservations.Errors;

public class ClientNotFoundError : Error
{
    public override string Type => nameof(ClientNotFoundError);
    public static ClientNotFoundError Instance => new();
}