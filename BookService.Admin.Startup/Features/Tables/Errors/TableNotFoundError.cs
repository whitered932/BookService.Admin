using Ftsoft.Common.Result;

namespace BookService.Admin.Startup.Features.Tables.Errors;

public class TableNotFoundError : Error
{
    public override string Type => nameof(TableNotFoundError);
    public static TableNotFoundError Instance => new ();
}